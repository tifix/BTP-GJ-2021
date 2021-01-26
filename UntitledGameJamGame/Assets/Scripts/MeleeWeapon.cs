using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeWeapon
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
        
        currentAttack = CreateAttackInstance(attacks[0]);

        //}
        //else
        //{
            
        //    currentAttack = CreateAttackInstance(attacks[0]);
        //}
        
    }

    private GameObject CreateAttackInstance(MeleeAttack attackType_)
    {
        GameObject attack = new GameObject("attack", typeof(SpriteRenderer), typeof(PolygonCollider2D));    //G tried to add monobehaviour here. That's illegal. Scripts are added though addcomponent
        attack.AddComponent<MeleeAttack>();
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
