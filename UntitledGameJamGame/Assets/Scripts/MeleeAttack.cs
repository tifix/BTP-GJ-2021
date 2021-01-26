using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    public Sprite sprite;
    public PolygonCollider2D hitbox;

    public Vector3 size= Vector3.one;
    public float minDuration;
    public float maxDuration = 4f;
    public float damage;
    public float minTimingInterval, maxTimingInterval;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = size;
        Destroy(gameObject, maxDuration);
        GetComponent<SpriteRenderer>().flipY = new bool[] { true, false }[Random.Range(0, 2)];
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health) && collision.gameObject != Player.instance.gameObject)
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
