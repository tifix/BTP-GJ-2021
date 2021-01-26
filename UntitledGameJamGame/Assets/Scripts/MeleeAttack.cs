using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    public Sprite sprite;
    public PolygonCollider2D hitbox;

    public float minDuration;
    public float maxDuration = 4f;
    public float damage;
    public float minTimingInterval, maxTimingInterval;

    // Start is called before the first frame update
    void Start()
    {  
        Destroy(gameObject, maxDuration);        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.TryGetComponent(out Health health) && col.gameObject != gameObject)
        {
            health.TakeDamage(damage);
        }
    }

    public void DestroyGameObject()
    {
        Debug.Log("It's been destroyed");
        Destroy(gameObject);
    }
}
