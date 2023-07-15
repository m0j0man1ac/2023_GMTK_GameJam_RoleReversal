using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class CardDisplay : MonoBehaviour
{
    public Card setCard;

    public TMP_Text cardName;
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
        cardName.text = card.name;
        description.text = card.desription;
        if(card.artIcon!=null) spriteR.sprite = card.artIcon;
    }
}
