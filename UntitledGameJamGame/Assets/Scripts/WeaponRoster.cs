using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRoster : MonoBehaviour
{
    public List<MeleeWeapon> meleeWeapons = new List<MeleeWeapon>(); //List of meleeWeapons that the user has
    public MeleeWeapon meleeWeapon = new MeleeWeapon(); //Melee weapon that the user is currently using

    //public List<RangedWeapon> rangedWeapons;    
}
