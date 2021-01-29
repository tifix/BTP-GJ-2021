using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dash_distance = 15;
    public float dash_cooldown = 1;
    public bool can_dash = true;
    public GameObject dash_particles;   //Particle system ROTATED TO EMMIT FORWARDS

    public IEnumerator Woooosh(Vector2 destination, bool cam_distort)
    {
        if (can_dash)
        {
            can_dash = false;
            if (cam_distort) { CamEffects.instance.CameraFOVDistort(0.09f, 0.12f, 0); AudioHandler.PlayEffect("blink1"); }
            if (dash_particles != null) SpawnDashParticles(destination);
            GetComponent<Rigidbody2D>().AddForce((destination - (Vector2)transform.position).normalized * dash_distance, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dash_cooldown);
            can_dash = true;
        }
        yield return null;
    }


    private void SpawnDashParticles(Vector2 destination)
    {
        GameObject particles = Instantiate(dash_particles, transform.position, Quaternion.FromToRotation(Vector3.forward, transform.position-(Vector3)destination));
        particles.transform.SetParent(transform);
        Destroy(particles, 1);
    }
}
