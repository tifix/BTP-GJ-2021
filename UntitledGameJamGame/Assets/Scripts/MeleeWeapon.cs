using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{    
    public List<MeleeAttack> attacks = new List<MeleeAttack>();
    public GameObject currentAttack;
    public MeleeAttack previousAttackInfo = new MeleeAttack();
    public int attackIndex = 0;

    public void Start()
    {
        currentAttack = null;
    }

    public void Swing()
    {
        if (currentAttack != null)
        {
            previousAttackInfo = currentAttack.GetComponent<MeleeAttack>();
            Destroy(currentAttack);
        }
        
        currentAttack = CreateAttackInstance(attacks[0]);

        //}
        //else
        //{
            
        //    currentAttack = CreateAttackInstance(attacks[0]);
        //}
        
    }

    private GameObject CreateAttackInstance(MeleeAttack attackType_)
    {
        GameObject attack = new GameObject("attack", typeof(MeleeAttack), typeof(SpriteRenderer), typeof(PolygonCollider2D));
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
