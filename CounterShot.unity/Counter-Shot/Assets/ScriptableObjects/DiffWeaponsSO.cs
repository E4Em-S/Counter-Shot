using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapons", menuName = "Different Weapons")]
public class DiffWeaponsSO : ScriptableObject
{
    public string weapons;
    public int numOfParries;
    public int dam;
    public GameObject bullet1;
}

//hopefully use this to create the different weapons, still new to scriptable objects tho