using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player movement script. Just for moving about. Works with both WSAD and Arrow keys. Requires an attached Rigidbody2D
 * 
 * For snappy movement, use high movement speed (400) with high linear drag in Rigidbody (4)
 * 
 */


[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    [Header("component references")]
    public Rigidbody2D RB;

    public float move_speed=2;

    void Update()
    {
        //Read player input
        GetPlayerInput();
    }

    void GetPlayerInput()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        //scaling to units per time, instead of units per frame
        hor *= Time.deltaTime;
        ver *= Time.deltaTime;
        //scaling by move_speed
        hor *= move_speed;
        ver *= move_speed;

        //applying movement
        RB.AddForce(Vector2.right * hor);
        RB.AddForce(Vector2.up * ver);
    }

}
