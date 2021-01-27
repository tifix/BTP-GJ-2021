using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    public Sprite sprite;
    public PolygonCollider2D hitbox;

    public Vector3 size= Vector3.one;

    //[Tooltip("interval between attacks")]

    //time until can attack again
    //time after which the collider disappears
    //time after the attack gameobject is destroyed - combo brea

    public float minAttackInterval;
    public float maxDuration = 4f;
    public float damage;

    [Tooltip("combo timing margins - smaller brackets for min attack, max dur")]
    public float minTimingInterval, maxTimingInterval;

    // Start is called before the first frame update
    public virtual void Start()
    {
        transform.localScale = size;
        Destroy(gameObject, maxDuration);
        GetComponent<SpriteRenderer>().flipY = new bool[] { true, false }[Random.Range(0, 2)];
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health) && collision.gameObject != Player.instance.gameObject)
        {
            health.TakeDamage(damage);
        }
    }
    public void Vanish()
    {

    }


    public virtual void DestroyGameObject()
    {
        Debug.Log("It's been destroyed");
        Destroy(gameObject);
    }
}
