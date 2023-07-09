using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum battleStates
    {
        START,
        PLAYERTURN,
        HEROTURN,
        WIN,
        LOSS
    }

    public battleStates battleState;

    void Update()
    {
        if(battleState == battleStates.PLAYERTURN)
        {

        }
    }
}
