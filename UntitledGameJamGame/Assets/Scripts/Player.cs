using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public MeleeWeapon weapon;
    WeaponCombo wc;    

    public float timeSinceLastAttack, timeOfLastAttack;

    void Start()
    {
        instance = this;
        wc = GetComponentInChildren<WeaponCombo>();
        weapon = GetComponentInChildren<MeleeWeapon>();
    }

    void Update()
    {
        timeSinceLastAttack = Time.deltaTime - timeOfLastAttack;

        if (Input.GetMouseButtonDown(0)) //Upon the mouse being down for 1 frame.
        {
            if (weapon.previousAttackInfo) //If there has been a previous attack with that weapon
            {
                if (timeSinceLastAttack > weapon.currentAttack.GetComponent<MeleeAttack>().minDuration) //If the time since the last attack is greater than the minimum duration of the previous attack
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
        weapon.attackIndex = 0;/* = wc.ComboProcessing();*/
        weapon.Swing();
        StartTimerUponAttack();
    }

    public void StartTimerUponAttack()
    {
        timeOfLastAttack = Time.deltaTime;
    }
}
