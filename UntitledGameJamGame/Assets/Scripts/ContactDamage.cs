using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Basic collision-based enemies.
 * Deal damage on contact and knock the player back a small amount.
 * Extend the class to add advanced movement patterns and such
 * 
 */

public class ContactDamage : MonoBehaviour
{
    public float contact_damage = 1;
    public float knockback_amount = 1;
    [Tooltip("does the enemy deal damage to destructible objects?")]
    public bool damage_props = true;
    [Tooltip("does the enemy deal damage to other enemies on contact?")]
    public bool damage_friendly = false;



    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Health target))
        {
            //if coming in contact with other enemies
            if (col.gameObject.CompareTag("Enemy") && damage_friendly == false) return;
            else if (col.gameObject.CompareTag("Enemy") && damage_friendly == true) { target.GetComponent<Health>().TakeDamage(contact_damage); return; }

            //if impacting player, deal damage and knock them back
            if (col.gameObject.CompareTag("Player"))
            {
                target.GetComponent<Health>().TakeDamage(contact_damage);

                //getting the direction in which the player should be knocked back
                Vector2 knockback_vector = -col.contacts[0].normal;

                //applying knockback force
                target.GetComponent<Rigidbody2D>().AddForce(knockback_vector.normalized * knockback_amount, ForceMode2D.Impulse);
                Debug.Log("Knockback");
                return;
            }

            //If coming in contact with non-players - smashing pots and all that
            if (damage_props) target.GetComponent<Health>().TakeDamage(contact_damage);
            else return;
        }

    }
}