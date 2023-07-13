using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType { Attack, Skill, Flavour };

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public new string name;

    public string cardType;

    
    public CardType cardTypeE = CardType.Attack;

    public Sprite artIcon;
    public string desription;
    public int energyCost;

    //public string cardType;

    public float dramaVal;
    public float braveryVal;

    public float damage;
    public float healing;

    public CardEffect[] effects;
}
