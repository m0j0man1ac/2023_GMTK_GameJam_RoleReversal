using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSaver : MonoBehaviour
{
    public static GameStateSaver instance;

    public int heroHealth, villainHealth;
    public int courageVal, dramaVal;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        instance = this;
    }

    public void SaveValues(int heroH, int villainH, int courage, int drama)
    {
        heroHealth = heroH;
        villainHealth = villainH;
        courageVal = courage;
        dramaVal = drama;
    }
}
