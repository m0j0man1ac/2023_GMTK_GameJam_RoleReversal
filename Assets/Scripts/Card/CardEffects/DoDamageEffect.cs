using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card Effect/Do Damage")]
public class DoDamageEffect : CardEffect
{
    public override void DoEffect(Card card)
    {
        HealthManagerScript.instance.HeroDamage((int)card.damage);
    }

    public override string EffectDescription(Card card)
    {
        return string.Format("Deal {0} Damage", card.damage);
    }
}
