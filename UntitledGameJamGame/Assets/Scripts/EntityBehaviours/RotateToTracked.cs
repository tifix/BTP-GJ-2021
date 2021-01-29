using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTracked : MonoBehaviour
{
    TrackObject tracker;
    Rigidbody2D RB;

    private void Awake()
    {
        if (tracker == null) tracker = GetComponent<TrackObject>();
        if (RB == null) RB = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        if (tracker.seing != null) transform.up =((Vector2)tracker.seing.transform.position - (Vector2)transform.position);       //RB.velocity; //Rotating to inverse velocity - bottom-facing sprites will face velocity
    }
}
