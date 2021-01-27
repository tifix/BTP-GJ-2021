using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public MeleeWeapon weapon;
    public float max_dmg=10;
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

        timeSinceLastAttack = Time.time - timeOfLastAttack;

        if (Input.GetMouseButtonDown(0)) //Upon the mouse being down for 1 frame.
        {
            if (weapon.currentAttack != null) //If there has been a previous attack with that weapon
            {
                if (timeSinceLastAttack > weapon.currentAttack.GetComponent<MeleeAttack>().minAttackInterval) //If the time since the last attack is greater than the minimum duration of the previous attack
                {
                    MeleeAttack();
                    return;
                }
                else
                {
                    Debug.Log("Case A - attack too early - not attacking");
                }
            }
            else //If there hasn't been a previous attack with that weapon
            {
                Debug.Log("Case B - attack too late, combo broken");
                MeleeAttack();
            }
        }
    }

    private void MeleeAttack()
    {

        weapon.attackIndex = wc.ComboProcessing();
        weapon.Swing();
        StartTimerUponAttack();
    }

    public void StartTimerUponAttack()
    {
        timeOfLastAttack = Time.time;
    }

}
