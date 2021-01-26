using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeIfNearby : MonoBehaviour
{
    public TrackObject tracker;
    private float detonation_distance=1.5f;
    private float explosion_range=2;
    private float detonation_fuse=1;
    private float damage = 5;
    public bool fuse_lit = false;

    void Start()
    {
        tracker = GetComponent<TrackObject>();
    }

    void FixedUpdate()
    {
        //if target comes too close - bust
        if (tracker.seing!=null && tracker.distance_to_seen < detonation_distance && fuse_lit==false) StartCoroutine(Burst());
    }

    private IEnumerator Burst()
    {
        fuse_lit = true;
        tracker.navmesh_base.agent.speed *= 0.1f;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(detonation_fuse);
        Collider2D[] impacted = Physics2D.OverlapCircleAll(transform.position, explosion_range);
        foreach(Collider2D victim in impacted)
        {
            Debug.Log(victim.name + " hit!");
            if(victim.gameObject.TryGetComponent<Health>(out Health h)) { h.TakeDamage(damage); }
        }
    }
    

}
