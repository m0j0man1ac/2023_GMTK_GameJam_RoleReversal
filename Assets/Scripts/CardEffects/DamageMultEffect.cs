using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DamageMultEffect : CardEffect
{
    public float damageMult = 2;

    public override void DoEffect()
    {
        //throw new System.NotImplementedException();
        Debug.Log(this.name + ": multiplying damage by " + damageMult);
        GameManagerScript.instance.villainAttackMult = damageMult;
    }
}
