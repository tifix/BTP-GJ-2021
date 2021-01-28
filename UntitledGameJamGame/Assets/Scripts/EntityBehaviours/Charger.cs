using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dash))]
[RequireComponent(typeof(TrackObject))]

public class Charger : MonoBehaviour
{
    private Dash dash;
    private TrackObject track;

    private void Awake()
    {
        dash = GetComponent<Dash>();
        track = GetComponent<TrackObject>();
    }


    private void FixedUpdate()
    {
        if (dash.can_dash &&track.seing!=null) StartCoroutine(dash.Woooosh(track.destination, false));
    }
}
