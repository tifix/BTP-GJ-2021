using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    private MeleeWeapon weapon;
    public Collider2D attackBox;

    public float minDuration, maxDuration;
    public float damage;
    public float minTimingInterval, maxTimingInterval;

    // Start is called before the first frame update
    void Start()
    {
        attackBox = new Collider2D();        
        Destroy(this, maxDuration);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {        
        if (col.gameObject.TryGetComponent(out Health health) && col.gameObject != gameObject)
        {
            health.TakeDamage(damage);
        }
    }
}
