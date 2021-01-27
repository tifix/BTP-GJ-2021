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

        //StartCoroutine(ContinousDashing());
    }

    // Update is called once per frame
    /*
    IEnumerator ContinousDashing()
    {
        yield return null;

        while (true)
        {
            
            yield return new WaitForFixedUpdate();

        }

    }
    */

    private void FixedUpdate()
    {
        if (dash.can_dash) StartCoroutine(dash.Woooosh(track.destination, false));
    }
}
