using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            DealCard();
        }

        if(Input.GetMouseButtonDown(1))
        {
            UpdateHandPositions();
        }
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

    public float cardAnimSpeed = .2f;
    public float cardAngleSpacing = 20;
    public float cardDistanceFromOrigin = 10;

    public void UpdateHandPositions()
    {
        int count = hand.Count;

        float arc = cardAngleSpacing * (count-1);
        float arcStart = -arc / 2;

        for (int i=0; i < count; i++)
        {
            //
            float rad = arcStart + (cardAngleSpacing * i) + 90;
            rad *= Mathf.Deg2Rad;
            Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad)).normalized;

            hand[i].DORotate(new Vector3(0, 0, arcStart + (cardAngleSpacing * i)), cardAnimSpeed);
            hand[i].DOMove(handOrigin.position + dir * cardDistanceFromOrigin, cardAnimSpeed);
        }
    }
}
