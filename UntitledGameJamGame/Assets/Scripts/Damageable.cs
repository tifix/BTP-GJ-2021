using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base class for all damagable entities - enemies, player and all
 * 
 */

public class Damageable : MonoBehaviour
{
    public float HP;
    public float max_HP;


    public virtual void TakeDamage(float damage)
    {
        if (HP - damage <= 0) Destroy(gameObject);
        else HP -= damage;
    }

    public void Heal(float amount)
    {
        if (HP + amount >= max_HP) HP=max_HP;
        else HP += amount;
    }

}
