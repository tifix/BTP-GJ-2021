using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public MeleeWeapon weapon;
    WeaponCombo wc;    

    public float timeSinceLastAttack, timeOfLastAttack;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {     
        wc = GetComponentInChildren<WeaponCombo>();
        weapon = GetComponentInChildren<MeleeWeapon>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) Time.timeScale = 0.2f;
        //if (Input.GetKeyUp(KeyCode.Space)) Time.timeScale = 1;

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
