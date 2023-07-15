using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManagerScript : MonoBehaviour
{
    public static TurnManagerScript instance;

    enum TurnState {Shuffle, Hero, Villain, Wait, Animating};
    TurnState currentTurn = TurnState.Shuffle;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTurn = TurnState.Villain;
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    public void EndTurn()
    {
        Debug.Log("End Turn");
        currentTurn = TurnState.Hero;
        HeroTurn();
    }

    public void HeroTurn()
    {
        Debug.Log("Did Hero's Turn");
    }
}
