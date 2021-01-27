using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCombo : MonoBehaviour
{
    Player p;
    public int comboIndex = 0;
    public int Attack_cycling_index=0;
    
    private void Start()
    {
       p= gameObject.GetComponentInParent<Player>();
    }

    public int ComboProcessing()
    {        
        if (p.timeSinceLastAttack >= p.weapon.attacks[p.weapon.attackIndex].minTimingInterval
                    && p.timeSinceLastAttack <= p.weapon.attacks[p.weapon.attackIndex].maxTimingInterval) //if the time since last attack is between the timing interval
        {
            //comboIndex++; //Add to the comboNumber
            Debug.Log("CRIT! DMG increased!");
            p.weapon.currentAttack.GetComponent<MeleeAttack>().MakeCritical();
        }
        else //If the time since the last attack is not within the PERFECT timing interval
        {
            Attack_cycling_index = 0; //return to default attack
        }

        return Attack_cycling_index;
    }

    public void HitLanded()
    {
        comboIndex++;
    }
    public void NoHitTimeOut()
    {
        comboIndex = 0; //Reset the combo 
        p.weapon.attacks[p.weapon.attackIndex].damage = 2;
    }



}
