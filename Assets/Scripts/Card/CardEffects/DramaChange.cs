using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Drama Change")]
public class DramaChange : CardEffect
{
    public override void DoEffect(Card card)
    {
        Bars.dramaInstance.ChangeValue((int)card.dramaVal);
    }

    public override string EffectDescription(Card card)
    {
        if(card.dramaVal>0)
            return string.Format("Increase Drama by {0}", card.dramaVal);
        else
            return string.Format("Decrease Drama by {0}", Mathf.Abs(card.dramaVal));
    }
}
