using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{    
    public List<MeleeAttack> attacks;
    public MeleeAttack currentAttack;/* = new MeleeAttack();*/
    public MeleeAttack previousAttack = new MeleeAttack();
    public int attackIndex;

    public void Swing()
    {
        //Debug.Log("Hello");
        //previousAttack = currentAttack;
        //currentAttack = new MeleeAttack();
        //currentAttack = attacks[attackIndex];

        currentAttack = Instantiate(attacks[0]);

        Debug.Log(currentAttack);

    }
}
