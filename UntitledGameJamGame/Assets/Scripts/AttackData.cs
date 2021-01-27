using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData : MeleeAttack
{

    public void StoreData(MeleeAttack attack)
    {
        damage = attack.damage;
        maxDuration = attack.maxDuration;
        minAttackInterval = attack.minAttackInterval;
        maxTimingInterval = attack.maxTimingInterval;
        minTimingInterval = attack.minTimingInterval;
    }
    public override void Start()
    {
        //Can't kill me bitch!
    }
}
