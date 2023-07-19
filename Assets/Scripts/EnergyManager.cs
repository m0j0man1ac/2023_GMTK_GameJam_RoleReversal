using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{
    public static EnergyManager instance;
    public List<Image> energyUnits;

    public int maxEnergy = 4, energy = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        energyUnits.Clear();
        energyUnits.AddRange(gameObject.GetComponentsInChildren<Image>());
        energy = maxEnergy;
    }

    public void ChangeEnergy(int value)
    {
        energy += value;
        UpdateEnergy();
    }

    void UpdateEnergy()
    {
        for(int i=0; i<energyUnits.Count; i++)
        {
            var visual = energyUnits[i];
            if (i < energy) visual.enabled = true;
            else visual.enabled = false;
        }
    }

    public void ClearEnergy()
    {
        ChangeEnergy(-energy);
    }

    public void MaxEnergy()
    {
        ChangeEnergy(-energy + maxEnergy);
    }

    private void OnValidate()
    {
        UpdateEnergy();
    }
}
