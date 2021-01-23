using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Basic collision-based enemies.
 * Deal damage on contact and knock the player back a small amount.
 * Extend the class to add advanced movement patterns and such
 * 
 */


public class EnemyAI : Damageable
{

    public GameObject hit_effect;

    public float contact_damage = 1;
    public float knockback_amount = 1;
    [Tooltip("does the enemy deal damage to destructible objects?")]
    public bool damage_props = true;
    [Tooltip("does the enemy deal damage to other enemies on contact?")]
    public bool damage_friendly = false;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Damageable>(out Damageable target))
        {
            //if coming in contact with other enemies
            if (target is EnemyAI && damage_friendly == false) return;
            else if (target is EnemyAI && damage_friendly == true) { target.GetComponent<EnemyAI>().TakeDamage(contact_damage); return; }

            //if impacting player, deal damage and knock them back
            if (target is PlayerHealth)
            {
                target.GetComponent<PlayerHealth>().TakeDamage(contact_damage);

                //getting the direction in which the player should be knocked back
                Vector2 knockback_vector = -collision.contacts[0].normal;

                //applying knockback force
                target.GetComponent<Rigidbody2D>().AddForce(knockback_vector.normalized * knockback_amount, ForceMode2D.Impulse);
                return;
            }

            //If coming in contact with non-players - smashing pots and all that
            if (damage_props) target.GetComponent<Damageable>().TakeDamage(contact_damage);
            else return;
        }

    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //Show hit particles
        GameObject hit_splash = Instantiate(hit_effect, transform);
        Destroy(hit_splash, 1);

    }
}