using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    private float dash_distance = 15;
    private float dash_cooldown = 1;
    private bool can_dash = true;

    public IEnumerator Woooosh(Vector2 destination, bool cam_distort)
    {
        if (can_dash)
        {
            can_dash = false;
            if(cam_distort) CamEffects.instance.CameraFOVDistort(0.09f, 0.12f, 0);
            GetComponent<Rigidbody2D>().AddForce((destination - (Vector2)transform.position).normalized * dash_distance, ForceMode2D.Impulse);
            yield return new WaitForSeconds(dash_cooldown);
            can_dash = true;
        }
        yield return null;
    }
}
