using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Base class for all damagable entities - enemies, player and all
 * 
 */

public class Health : MonoBehaviour
{
    public float HP;
    public float max_HP;
    public GameObject hit_effect;


    public virtual void TakeDamage(float damage)
    {
        //Show hit particles
        GameObject hit_splash = Instantiate(hit_effect, transform);
        Destroy(hit_splash, 1);

        //apply damage
        if (HP - damage <= 0) Destroy(gameObject);
        else HP -= damage;
    }

    public void Heal(float amount)
    {
        if (HP + amount >= max_HP) HP=max_HP;
        else HP += amount;
    }

}
