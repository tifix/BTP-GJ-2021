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
            comboIndex++; //Add to the comboNumber


            //Attack cycling
            if (Attack_cycling_index < p.weapon.attacks.Count-1) //If the comboNumber is less than the number of attacks on the weapon
            {
                Attack_cycling_index++;
            }
            else //If the comboNumber is greater than or equal to the amount of attacks
            {
                Attack_cycling_index = 0;
            }
        }
        else //If the time since the last attack is not within the timing interval
        {
            comboIndex = 0; //Reset the combo 
            Attack_cycling_index = 0; //return to default attack
        }

        return Attack_cycling_index;
    }


}
