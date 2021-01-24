using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{    
    public List<MeleeAttack> attacks;
    public MeleeAttack currentAttack;
    public MeleeAttack previousAttack;
    public int attackIndex;

    public void Swing()
    {
        previousAttack = currentAttack;
        currentAttack = new MeleeAttack();
        currentAttack = attacks[attackIndex];
    }
}
