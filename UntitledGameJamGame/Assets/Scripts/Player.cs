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
            if (roster == null)
            {
                Debug.Log("Roster is null");
            }

            if (roster.meleeWeapon == null)
            {
                Debug.Log("meleeweapon is null");
            }

            if (roster.meleeWeapon.previousAttack == null)
            {
                Debug.Log("previousattack is null");
            }

            if (roster.meleeWeapon.previousAttack != null)
            {
                if (timeSinceLastAttack > roster.meleeWeapon.previousAttack.minDuration) //If the time since the last attack is greater than the minimum duration of the previous attack
                {
                    MeleeAttack();
                }
            }
            else
            {
                MeleeAttack();
            }
        }
    }

    private void MeleeAttack()
    {

        //if (roster.meleeWeapon.attacks[roster.meleeWeapon.attackIndex] == null)
        //{
        //    Debug.Log("p.roster.meleeWeapon.attacks[p.roster.meleeWeapon.attackIndex] is null");
        //}

        if (roster.meleeWeapon.attacks[0] == null)
        {
            Debug.Log("p.roster.meleeWeapon.attacks[0] is null");
        }

        roster.meleeWeapon.attackIndex = 0;/* = wc.ComboProcessing();*/
        roster.meleeWeapon.Swing();
        StartTimerUponAttack();
    }

    public void StartTimerUponAttack()
    {
        timeOfLastAttack = Time.deltaTime;
    }
}
