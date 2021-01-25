using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowPlayer : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform player;
    private float detection_radius = 5;
    private float check_interval = 0.2f;
    [Tooltip("position to which return after player walks too far")]
    private Vector2 spawn_position;

    void Start()
    {
        spawn_position = transform.position;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        StartCoroutine(CheckIfPlayerNearbyLoop());
    }


    IEnumerator CheckIfPlayerNearbyLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(check_interval);
            if (Vector2.Distance(player.transform.position, transform.position) > 50) yield return new WaitForSeconds(1);   //Refreshes less often if the plaeyr is far away.
            CheckIfPlayerIsNearby();
        }
    }


    //Scan nearby area looking for player. Follow player if he's nearby, otherwise return to base position
    void CheckIfPlayerIsNearby()
    {
        foreach(Collider2D entity in Physics2D.OverlapCircleAll(transform.position, detection_radius)) 
        {
            if (entity.gameObject.CompareTag("Player"))
            {
                if (player == null) //if player is just spotted, update agent destination
                {
                    player = entity.transform;
                    UpdateDestination(); 
                } 
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
        if (player != null) agent.destination = player.position;
        else agent.destination = spawn_position;
    }

}


