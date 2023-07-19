using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Courage Change")]
public class CourageChange : CardEffect
{
    public override void DoEffect(Card card)
    {
        Bars.courageInstance.ChangeValue((int)card.braveryVal);
    }

    public override string EffectDescription(Card card)
    {
        if(card.braveryVal >0)
            return string.Format("Increase Courage by {0}", card.braveryVal);
        else
            return string.Format("Decrease Courage by {0}", Mathf.Abs(card.braveryVal));
    }
}
