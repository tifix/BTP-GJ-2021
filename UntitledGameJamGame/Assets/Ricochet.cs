using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Ricochet : MonoBehaviour
{
    public float velocity_retained = 1;    //if 1, bounce off with 100% speed at impact

    private Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ray2D ricochet_trajectory = new Ray2D(collision.GetContact(0).point, collision.GetContact(0).normal.normalized);
        Debug.DrawLine(ricochet_trajectory.origin, ricochet_trajectory.origin + ricochet_trajectory.direction, Color.blue,1f);

        RB.AddForce(ricochet_trajectory.direction * collision.relativeVelocity* velocity_retained,ForceMode2D.Impulse);
    }
}
