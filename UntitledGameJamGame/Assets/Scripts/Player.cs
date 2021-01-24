using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public WeaponRoster roster;
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
            if (timeSinceLastAttack > roster.meleeWeapon.previousAttack.minDuration) //If the time since the last attack is greater than the minimum duration of the previous attack
            {                
                MeleeAttack();
            }
        }
    }

    private void MeleeAttack()
    {
        roster.meleeWeapon.attackIndex = wc.ComboProcessing();
        roster.meleeWeapon.Swing();
        StartTimerUponAttack();
    }

    public void StartTimerUponAttack()
    {
        timeOfLastAttack = Time.deltaTime;
    }
}
