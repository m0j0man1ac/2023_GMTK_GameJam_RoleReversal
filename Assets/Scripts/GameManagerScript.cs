using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public int handSize = 4;

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
        if (hand.Count >= 4) return;

        Transform dealtCard = GameObject.Instantiate(cardPrefab).transform;

        Debug.Log("dealing " + deck.Peek());
        dealtCard.GetComponent<CardDisplay>().setCard = deck.Peek();
        dealtCard.GetComponent<CardDisplay>().CardUpdate(deck.Dequeue());

        hand.Add(dealtCard);
        UpdateHandPositions();
    }

    public Transform handOrigin;
    public float cardAngleSpacing = 20;
    public float cardDistanceFromOrigin = 10;

    public void UpdateHandPositions()
    {
        int count = hand.Count;

        float arc = cardAngleSpacing * (count-1);
        float arcStart = -arc / 2;

        for(int i=0; i < count; i++)
        {
            hand[i].rotation = Quaternion.Euler(0,0,arcStart+(cardAngleSpacing*i));
            hand[i].position = handOrigin.position + hand[i].transform.up * cardDistanceFromOrigin;
        }
    }
}
