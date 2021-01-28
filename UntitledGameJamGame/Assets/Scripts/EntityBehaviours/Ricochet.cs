using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class Ricochet : MonoBehaviour
{
    public float velocity_retained = 1;    //set to Dash distance for dashers

    private Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Take 1

        /*Ray2D ricochet_trajectory = new Ray2D(collision.GetContact(0).point, collision.GetContact(0).normal.normalized);
        Debug.DrawLine(ricochet_trajectory.origin, ricochet_trajectory.origin + ricochet_trajectory.direction, Color.blue,1f);
        RB.AddForce(ricochet_trajectory.direction * collision.relativeVelocity* velocity_retained,ForceMode2D.Impulse);*/

        //this in theory is the way to do it. Look into it
        

        Vector2 inDirection = -RB.velocity;
        Vector2 inNormal = collision.contacts[0].normal.normalized;
        Vector2 newVelocity = Vector2.Reflect(inDirection, inNormal);
        RB.velocity = newVelocity.normalized*velocity_retained;

        //Take 2

        //Vector2 direction = Vector2.Reflect(RB.velocity.normalized, collision.GetContact(0).normal.normalized);
        //RB.AddForce(direction*velocity_retained,ForceMode2D.Impulse);
    }
}
