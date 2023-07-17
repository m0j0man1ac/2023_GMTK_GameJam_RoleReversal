using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card setCard;

    public TMP_Text cardName;
    public TMP_Text cardEnergy;
    public TMP_Text description;
    public SpriteRenderer spriteR;

    // Start is called before the first frame update
    void Start()
    {
        //if(setCard!=null)
          //  CardUpdate(setCard);
    }

    public void CardUpdate(Card card)
    {
        setCard = card;

        cardName.text = card.name;
        cardEnergy.text = card.energyCost.ToString();

        var descriptionsString = card.desription;
        foreach (CardEffect effect in card.effects ?? Enumerable.Empty<CardEffect>())
        {
            if (!string.Equals(descriptionsString, "")) descriptionsString += "\n";
            descriptionsString += effect.EffectDescription(card);
        }

        description.text = descriptionsString;
        if(card.artIcon!=null) spriteR.sprite = card.artIcon;
    }
}
