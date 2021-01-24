using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCombo : MonoBehaviour
{
    Player p;
    public int comboIndex = 0;
    
    private void Start()
    {
        gameObject.TryGetComponent(out p);
    }

    public int ComboProcessing()
    {
        if (p.timeSinceLastAttack >= p.roster.meleeWeapon.attacks[p.roster.meleeWeapon.attackIndex].minTimingInterval
                    && p.timeSinceLastAttack <= p.roster.meleeWeapon.attacks[p.roster.meleeWeapon.attackIndex].maxTimingInterval) //if the time since last attack is between the timing interval
        {
            if (comboIndex < p.roster.meleeWeapon.attacks.Count) //If the comboNumber is less than the number of attacks on the weapon
            {
                comboIndex++; //Add to the comboNumber
            }
            else if (comboIndex >= p.roster.meleeWeapon.attacks.Count) //If the comboNumber is greater than or equal to the amount of attacks
            {
                comboIndex = 0; //Reset the combo
            }
        }
        else //If the time since the last attack is not within the timing interval
        {
            comboIndex = 0; //Reset the combo 
        }

        return comboIndex;
    }


}
