using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class TrackObject : MonoBehaviour
{

    public GameObject looking_for;
    public GameObject seing;

    public  float detection_radius = 7;
    private float check_interval = 0.2f;
    [HideInInspector] public float distance_to_seen;

    [Tooltip("position to which return after looking_for walks too far")]
    private Vector2 spawn_position;
    public bool follow_tracked;
    [HideInInspector] public Vector2 destination;
    [HideInInspector] public UnityEvent target_spotted;

    
    public MoveOnNavMesh navmesh_base;

    void Start()
    {
        looking_for = Player.instance.gameObject;
        navmesh_base = GetComponent<MoveOnNavMesh>();
        spawn_position = transform.position;
        StartCoroutine(CheckIfTargetNearbyLoop());
    }



    IEnumerator CheckIfTargetNearbyLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(check_interval);
            CheckIfTargetIsNearby();
        }
    }


    //Scan nearby area looking for looking_for. Follow looking_for if he's nearby, otherwise return to base position
    void CheckIfTargetIsNearby()
    {
        foreach (Collider2D entity in Physics2D.OverlapCircleAll(transform.position, detection_radius))
        {
            if (entity.gameObject == looking_for)
            {
                if (seing == null) //if looking_for is just spotted, update agent destination
                {
                    seing = entity.gameObject;
                    target_spotted.Invoke();
                }

                distance_to_seen = Vector2.Distance(seing.transform.position, transform.position);
                UpdateDestination();
                return;
            }

        }
        if (seing != null)
        {
            seing = null;
            UpdateDestination();     //If the looking_for just walks out of range, update agent destination
        }
        return;
    }

    void UpdateDestination()
    {
        if (seing != null) destination = seing.transform.position;
        else destination = spawn_position;

        if (follow_tracked && navmesh_base.agent.isActiveAndEnabled && navmesh_base.agent.isOnNavMesh) navmesh_base.agent.destination = destination; 
    }

}


