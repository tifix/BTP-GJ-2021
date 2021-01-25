using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldFalloff : MonoBehaviour
{
    public Collider2D col;
    private Vector2 com;

    private void OnTriggerStay2D(Collider2D collision)
    {
        com = collision.bounds.center;

        if (col.OverlapPoint(com))
        {
            Debug.Log("over the edge");
            if (collision.gameObject.TryGetComponent<Health>(out Health h))
            {
                h.TakeDamage(999);
            }
        } 
    }



    public void OnDrawGizmos()
    {
        if (com != null) Gizmos.DrawSphere(com, 0.05f);
    }


}
