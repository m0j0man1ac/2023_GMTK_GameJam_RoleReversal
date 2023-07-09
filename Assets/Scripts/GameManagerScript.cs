using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Rendering;

using DG.Tweening;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public int handSize = 5;

    public List<Card> allCards = new List<Card>();

    public Queue<Card> deck = new Queue<Card>();
    public List<Card> discard = new List<Card>();

    public List<Transform> hand = new List<Transform>();

    public GameObject cardPrefab;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;

        allCards.AddRange(Resources.LoadAll<Card>("Cards"));

        while (allCards.Count > 0)
        {
            int idx = Random.Range(0, allCards.Count);

            Debug.Log("adding " + allCards[idx] + " to deck");

            deck.Enqueue(allCards[idx]);
            allCards.RemoveAt(idx);
        }
    }

    private void Start()
    {
        StartCoroutine(DealHand());
    }

    // Update is called once per frame
    void Update()
    {
        CheckMousedCards();

        if (Input.GetMouseButtonDown(0)) 
        {
            PickUpCard();
            //StartCoroutine(DealHand());
        }

        if(Input.GetMouseButtonUp(0))
        {
            PutDownCard();
        }

        if(Input.GetMouseButtonDown(1))
            UpdateHandPositions();

        if(heldCard!=null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            heldCard.position = Vector2.Lerp(heldCard.position, mousePos, Time.deltaTime*20);
        }
    }

    public float timeBetweenDealtCards = .1f;

    public IEnumerator DealHand()
    {
        while(hand.Count<handSize)
        {
            DealCard();
            yield return new WaitForSeconds(timeBetweenDealtCards);
        }

        yield return null;
    }

    public void DealCard()
    {
        if (deck.Count <= 0) return;
        if (hand.Count >= handSize) return;

        Transform dealtCard = GameObject.Instantiate(cardPrefab, deckT.position, Quaternion.Euler(0,0,90)).transform;

        Debug.Log("dealing " + deck.Peek());
        dealtCard.GetComponent<CardDisplay>().setCard = deck.Peek();
        dealtCard.GetComponent<CardDisplay>().CardUpdate(deck.Dequeue());

        hand.Add(dealtCard);
        UpdateHandPositions();
    }

    public Transform deckT, discardT;
    public Transform handOrigin;

    public int cardSortLayer = 10;

    public float cardAnimSpeed = .2f;
    public float cardAngleSpacing = 20;
    public float cardDistanceFromOrigin = 10;

    Vector3 cardScale = new Vector3(3, 3, 1);
    public Vector3 zoomedCardScale = new Vector3(4,4,1);

    int handCount;
    float arc;
    float arcStart;

    public void UpdateHandPositions()
    {
        handCount = hand.Count;

        arc = cardAngleSpacing * (handCount - 1);
        arcStart = -arc / 2;

        for (int i=0; i < handCount; i++)
        {
            hand[i].GetComponent<SortingGroup>().sortingOrder = cardSortLayer;
            //
            float rad = arcStart + (cardAngleSpacing * i) + 90;
            rad *= Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            hand[i].DORotate(new Vector3(0, 0, arcStart + (cardAngleSpacing * i)), cardAnimSpeed).SetEase(Ease.OutQuad);
            hand[i].DOMove(handOrigin.position + dir * cardDistanceFromOrigin, cardAnimSpeed).SetEase(Ease.OutQuad);

            if (hand[i].localScale != cardScale)
                hand[i].DOScale(cardScale, cardAnimSpeed);
        }
    }

    public void ResetCardPositionInHand(int i)
    {
        handCount = hand.Count;

        arc = cardAngleSpacing * (handCount - 1);
        arcStart = -arc / 2;

        hand[i].GetComponent<SortingGroup>().sortingOrder = cardSortLayer;
        //
        float rad = arcStart + (cardAngleSpacing * i) + 90;
        rad *= Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        hand[i].DORotate(new Vector3(0, 0, arcStart + (cardAngleSpacing * i)), cardAnimSpeed).SetEase(Ease.OutQuad);
        hand[i].DOMove(handOrigin.position + dir * cardDistanceFromOrigin, cardAnimSpeed).SetEase(Ease.OutQuad);

        if (hand[i].localScale != cardScale)
            hand[i].DOScale(cardScale, cardAnimSpeed);
    }

    public List<Collider2D> hoveredCards = new List<Collider2D>();
    public LayerMask cardMask;
    public Collider2D activeCard;

    public void CheckMousedCards()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        hoveredCards.Clear();
        hoveredCards.AddRange(Physics2D.OverlapPointAll(mousePos, cardMask.value));

        if (heldCard!=null) return;

        if (activeCard == null && hoveredCards.Count > 0)
        {
            activeCard = hoveredCards[0];
            StartHover(activeCard.transform);
        }
            
        if(activeCard==null) return;
        if(!hoveredCards.Contains(activeCard))
        {
            if (hand.Contains(activeCard.transform))
            {
                ResetCardPositionInHand(hand.IndexOf(activeCard.transform));
            }

            if (hoveredCards.Count > 0)
            {
                activeCard = hoveredCards[0];
                StartHover(activeCard.transform);
            }
            else
            {
                activeCard = null;
                UpdateHandPositions();
            }
        }
    }

    public void StartHover(Transform hoveredCard)
    {
        int idx = hand.IndexOf(hoveredCard);
        if (idx == -1) return;

        hoveredCard.GetComponent<SortingGroup>().sortingOrder += 1;

        float arc = cardAngleSpacing * (hand.Count - 1);
        float arcStart = -arc / 2;
        float rad = arcStart + (cardAngleSpacing * idx) + 90;
        rad *= Mathf.Deg2Rad;
        Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

        hand[idx].DORotate(Vector3.zero, cardAnimSpeed/2).SetEase(Ease.OutQuad);
        hand[idx].DOMove(handOrigin.position + dir * cardDistanceFromOrigin*1.1f - Vector3.forward, cardAnimSpeed/2).SetEase(Ease.OutQuad);
    }


    ///PLAYING A CARD

    public Transform heldCard;
    public void PickUpCard()
    {
        if (activeCard == null)
        {
            Debug.Log("no card to pickup");
            return;
        }

        heldCard = activeCard.transform;
    }

    public void PutDownCard()
    {
        if(heldCard==null)
        {
            Debug.Log("no card to play");
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.mousePosition.y > Screen.height / 3)
        {
            hand.Remove(heldCard);
            discard.Add(heldCard.GetComponent<CardDisplay>().setCard);
            GameObject.Destroy(heldCard.gameObject, .5f);
        }
        else
        {
            ResetCardPositionInHand(hand.IndexOf(heldCard));
        }

        heldCard = null;
    }
}
