using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MoveOnNavMesh))]
public class FollowPlayer : MonoBehaviour
{

    public Transform player;
    private float detection_radius = 5;
    private float check_interval = 0.2f;
    [Tooltip("position to which return after player walks too far")]
    private Vector2 spawn_position;

    public MoveOnNavMesh navmesh_base;

    void Start()
    {
        navmesh_base = GetComponent<MoveOnNavMesh>();
        spawn_position = transform.position;
        StartCoroutine(CheckIfPlayerNearbyLoop());
    }



    IEnumerator CheckIfPlayerNearbyLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(check_interval);
            CheckIfPlayerIsNearby();
        }
    }


    //Scan nearby area looking for player. Follow player if he's nearby, otherwise return to base position
    void CheckIfPlayerIsNearby()
    {
        foreach (Collider2D entity in Physics2D.OverlapCircleAll(transform.position, detection_radius))
        {
            if (entity.gameObject.CompareTag("Player"))
            {
                if (player == null) //if player is just spotted, update agent destination
                {
                    player = entity.transform;
                }
                UpdateDestination();
                return;
            }

        }
        if (player != null)
        {
            player = null;
            UpdateDestination();     //If the player just walks out of range, update agent destination
        }
        return;
    }

    void UpdateDestination()
    {
        if (player != null) navmesh_base.agent.destination = player.position;
        else navmesh_base.agent.destination = spawn_position;
    }

}


