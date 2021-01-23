using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{
    [Header("out of combat, passive healing parameters")]
    public bool out_of_combat = false;
    public float passive_heal_interval=0.5f;
    public float passive_heal_amount = 0.5f;
    public float time_to_start_healing = 5;

    [Header("current timer until out-of-combat healing starts")]
    public float time_remaining;

    public GameObject hit_effect;


    public void Update()
    {
        //Debug utility - asterisk to take damage
        if (Input.GetKeyDown(KeyCode.Asterisk)) TakeDamage(1);
    }

    //If the player does not take damage for set amount of time, he will heal
    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        //Show hit particles
        GameObject hit_splash = Instantiate(hit_effect, transform);
        Destroy(hit_splash, 1);

        //Stop healing and reset the out of combat timer
        out_of_combat = false;

        //Shake the camera
        CamEffects.instance.CameraShake();

        StopAllCoroutines();
        StartCoroutine(OutOfCombatTimer());
    }


    //Heals the player in set intervals, until health is maxed, or player takes a hit
    public IEnumerator PassiveHeal()
    {
        //heal the player in intervals untill his health is maxed out
        while (HP<max_HP)
        {
            yield return new WaitForSeconds(passive_heal_interval);
            Heal(passive_heal_amount);
        }
    }

    //This timer is reset whenever a player takes a hit. If the player does not take a hit in the specified time, he will start healing.
    public IEnumerator OutOfCombatTimer()
    {
        //starting the timer
        time_remaining = time_to_start_healing;
        while (time_remaining > 0)
        {
            yield return new WaitForSeconds(0.1f);
            time_remaining -= 0.1f;
        }

        //once the timer reaches 0, start healing
        out_of_combat = true;
        StartCoroutine(PassiveHeal());
    }

}
