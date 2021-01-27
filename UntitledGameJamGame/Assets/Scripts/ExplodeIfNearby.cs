using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExplodeIfNearby : MonoBehaviour
{
    public TrackObject tracker;
    public Health h;


    private float detonation_distance=1.5f;
    private float explosion_range=2;
    private float detonation_fuse=1;
    private float damage = 5;
    public bool fuse_lit = false;


    void Start()
    {
        tracker = GetComponent<TrackObject>();
        h = GetComponent<Health>();
    }

    void FixedUpdate()
    {
        //if target comes too close - bust
        if (tracker.seing!=null && tracker.distance_to_seen < detonation_distance && fuse_lit==false) StartCoroutine(ChargeAndPop());
    }

    private IEnumerator ChargeAndPop()
    {
        fuse_lit = true;
        tracker.navmesh_base.agent.speed *= 0.1f;
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(detonation_fuse);
        h.TakeDamage(999);
    }

    private void Explode()
    {
        Collider2D[] impacted = Physics2D.OverlapCircleAll(transform.position, explosion_range);
        foreach (Collider2D victim in impacted)
        {
            if (victim.gameObject.TryGetComponent<Health>(out Health helt)) helt.TakeDamage(damage);
        }
    }

    private void OnDestroy()
    {
        Explode();
    }


}
