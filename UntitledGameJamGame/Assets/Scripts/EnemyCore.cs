using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyCore : MonoBehaviour
{
    // public Dictionary<UnityEvent, IEnemyBehaviour> behaviours = new Dictionary<UnityEvent, IEnemyBehaviour>();
    public List<IEnemyBehaviour> behaviours = new List<IEnemyBehaviour>();

    public UnityEvent on_spot_player;
    public UnityEvent on_player_out_of_range;

    TrackObject tracker;

    private void Awake()
    {
        tracker = GetComponent<TrackObject>();
    }

    void FixedUpdate()
    {
        /*
        foreach(UnityEvent key in behaviours.Keys)
        {
            IEnemyBehaviour action;
            behaviours.TryGetValue(key, out action);
            if(action!=null) key.AddListener(action.Execute);


        }
        */
    }
}
