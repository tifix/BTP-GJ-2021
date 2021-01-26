using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MeleeWeapon : MonoBehaviour
{    
    public List<MeleeAttack> attacks = new List<MeleeAttack>();
    public GameObject currentAttack = null;
    public MeleeAttack previousAttackInfo;// = new MeleeAttack();
    public int attackIndex = 0;

    public void Swing()
    {
        if (currentAttack != null)
        {
            previousAttackInfo = currentAttack.GetComponent<MeleeAttack>();
            previousAttackInfo.DestroyGameObject();
        }
        
        currentAttack = CreateAttackInstance(attacks[Random.Range(0, attacks.Count)]);

        //}
        //else
        //{
            
        //    currentAttack = CreateAttackInstance(attacks[0]);
        //}
        
    }

    private GameObject CreateAttackInstance(MeleeAttack attackType_)
    {

        //new GameObject("attack", typeof(SpriteRenderer), typeof(PolygonCollider2D));      new syntax. Didn't know you could do that
        Vector2 attack_rotation = Camera.main.ScreenToWorldPoint(Input.mousePosition) -transform.position;

        GameObject attack = GameObject.Instantiate(Resources.Load("TestAttack") as GameObject, transform.position, Quaternion.identity);   //G tried to add monobehaviour here. That's illegal. Scripts are added though addcomponent
        attack.transform.right = -attack_rotation;
        MeleeAttack ma = attack.GetComponent<MeleeAttack>();
        SpriteRenderer sr = attack.GetComponent<SpriteRenderer>();
        PolygonCollider2D pc = attack.GetComponent<PolygonCollider2D>();

        attack.name = ("" + Time.deltaTime); //Just to show that it is a new gameobject being created
        ma = attackType_;
        sr.sprite = attackType_.sprite;
        pc = attackType_.hitbox;

        return attack;
    }

}
