using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public new string name;
    public string cardType;
    public Sprite artIcon;
    public string desription;
    public int energyCost;

    //public string cardType;

    public float dramaVal;
    public float braveryVal;

    public float damage;
    public float healing;
}
