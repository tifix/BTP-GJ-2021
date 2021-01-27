using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeAttack : MonoBehaviour
{
    public MeleeWeapon weapon;
    public WeaponCombo combo;
    public Sprite sprite;
    public PolygonCollider2D hitbox;

    public Vector3 size= Vector3.one;
    public bool is_crit=false;

    //time until can attack again
    public float pure_attack_duration;//time after which the collider disappears - attack is very short, unlike the combo intervals
    //time after the attack gameobject is destroyed - combo brea

    public float minAttackInterval;
    public float maxDuration = 4f;
    public float damage;

    [Tooltip("combo timing margins - smaller brackets for min attack, max dur")]
    public float minTimingInterval, maxTimingInterval;


    public virtual void Start()
    {
        weapon = Player.instance.gameObject.GetComponentInChildren<MeleeWeapon>();
        combo = Player.instance.gameObject.GetComponentInChildren<WeaponCombo>();
        transform.localScale = size;
        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer SR))
        {
            if (is_crit == true) SR.color = Color.blue;
            else SR.color = ChangeColourByDamage(damage);      //Tint the attack for combo attacks
        }

        Invoke("DestroyGameObject", maxDuration);
        Invoke("Vanish", pure_attack_duration);
        GetComponent<SpriteRenderer>().flipY = new bool[] { true, false }[Random.Range(0, 2)];
    }
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health) && collision.gameObject != Player.instance.gameObject)
        {
            health.TakeDamage(damage);
            combo.HitLanded();
        }
    }
    public void Vanish()
    {
        Destroy(GetComponent<Collider2D>());
        Destroy(GetComponent < SpriteRenderer>());
    }

    private Color ChangeColourByDamage(float damage)
    {
        Color a = Color.white;
        Color b = Color.blue;
        float c = Mathf.InverseLerp(2, Player.instance.max_dmg, damage);

        return Color.Lerp(a, b, c);

    }

    public virtual void DestroyGameObject()
    {
        //Debug.Log("It's been destroyed");
        combo.NoHitTimeOut();
        Destroy(gameObject);
    }
    public void MakeCritical()
    {
        is_crit = true;
        float dmg = weapon.attacks[weapon.attackIndex].damage;
        dmg += 1;
        dmg = Mathf.Clamp(dmg, 0, Player.instance.max_dmg);
        Debug.Log("dmg now " + damage);
        weapon.attacks[weapon.attackIndex].damage = dmg;
    }



}
