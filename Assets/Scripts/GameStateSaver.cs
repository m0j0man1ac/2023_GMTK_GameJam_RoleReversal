using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateSaver : MonoBehaviour
{
    public static GameStateSaver instance;

    public int heroHealth = 0, villainHealth = 0;
    public int courageVal = 0, dramaVal = 0;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public void SaveValues(int heroH, int villainH, int courage, int drama)
    {
        heroHealth = heroH;
        villainHealth = villainH;
        courageVal = courage;
        dramaVal = drama;
    }
}
