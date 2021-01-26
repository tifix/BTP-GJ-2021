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
        if(TryGetComponent<HealthRegen>(out HealthRegen HR))
        {
            HR.DamageTaken();
        }

        //Show hit particles
        GameObject hit_splash = Instantiate(hit_effect, transform);
        hit_splash.transform.SetParent(null);
        Destroy(hit_splash, 1);

        //apply damage
        if (HP - damage <= 0)
        {
            //if killing player, defeat instead of destroying
            if (gameObject== Player.instance.gameObject) CamEffects.instance.Defeat();
            else Destroy(gameObject);

        }
        else HP -= damage;
    }

    public void Heal(float amount)
    {
        if (HP + amount >= max_HP) HP=max_HP;
        else HP += amount;
    }

}
