using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    Attacking,
    Debuff,
    Mono,
    Shield,
    PlayerAttacked,
    HeroLow,
    HeroMid,
    HeroChad
};

[CreateAssetMenu]
public class Card : ScriptableObject
{
    public new string name;

    public CardType cardType;
    public Sprite artIcon;
    public string desription;
    public int energyCost;

    public DialougeGroup dialougeGroup;
    //public string cardType;

    public float dramaVal;
    public float braveryVal;

    public float damage;
    public float healing;

    public CardEffect[] effects;
}
