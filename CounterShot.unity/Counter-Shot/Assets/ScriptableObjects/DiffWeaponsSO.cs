using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Obj/WeaponData")]
public class DiffWeaponsSO : ScriptableObject
{
    public string weaponName;
    public int numOfParriesfor1;
    public int bulletSpeed;
    public float fireRate;
    public int damage;
    public GameObject projectilePrefab;
}

//hopefully use this to create the different weapons, still new to scriptable objects tho