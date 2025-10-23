using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PistolAmmoUI : MonoBehaviour
{
    [SerializeField] GameObject pistolAmmo;
    int bullets;
    public void OnSwitchWeapons(InputValue value)
    {

    }
    public void RemoveBulletPistolUI()
    {
        bullets = pistolAmmo.transform.childCount;
        for (int i = bullets - 1; i >= bullets - 1; i--)
        {
            
        }
    }
}
