using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public Card[] allCards;

    public List<Card> deck = new List<Card>();
    public List<Card> discard = new List<Card>(); 

    // Start is called before the first frame update
    void Start()
    {
        allCards = Resources.LoadAll<Card>("Cards");

        foreach (Card card in allCards)
        {
            deck.Add(card);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
