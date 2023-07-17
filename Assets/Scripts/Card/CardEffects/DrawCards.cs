using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Threading.Tasks;

[CreateAssetMenu (menuName ="Card Effect/Draw Cards")]
public class DrawCards : CardEffect
{
    public int numCards;

    public override void DoEffect(Card card)
    {
        var gmInstance = GameManagerScript.instance;
        for (int i=0; i<numCards; i++)
        {
            gmInstance.Invoke("DealCard", i*gmInstance.timeBetweenDealtCards);
        }
    }

    public override string EffectDescription(Card card)
    {
        if (numCards == 1) return string.Format("Draw {0} card.", numCards); 
        else return string.Format("Draw {0} cards.", numCards);
    }
}
