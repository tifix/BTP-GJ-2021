using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public WeaponRoster roster = new WeaponRoster();
    WeaponCombo wc;    

    public float timeSinceLastAttack, timeOfLastAttack;

    void Start()
    {
        wc = GetComponent<WeaponCombo>();
    }

    void Update()
    {
        timeSinceLastAttack = Time.deltaTime - timeOfLastAttack;

        if (Input.GetMouseButtonDown(0)) //Upon the mouse being down for 1 frame.
        {
            if (roster.meleeWeapon.previousAttackInfo) //If there has been a previous attack with that weapon
            {
                if (timeSinceLastAttack > roster.meleeWeapon.currentAttack.GetComponent<MeleeAttack>().minDuration) //If the time since the last attack is greater than the minimum duration of the previous attack
                {
                    MeleeAttack();
                }
            }
            else //If there hasn't been a previous attack with that weapon
            {
                MeleeAttack();
            }
        }
    }

    private void MeleeAttack()
    {
        roster.meleeWeapon.attackIndex = 0;/* = wc.ComboProcessing();*/
        roster.meleeWeapon.Swing();
        StartTimerUponAttack();
    }

    public void StartTimerUponAttack()
    {
        timeOfLastAttack = Time.deltaTime;
    }
}
