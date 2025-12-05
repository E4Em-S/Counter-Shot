using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class WeaponChanger : MonoBehaviour
{
    int totalWeapons;

    public int currentWeaponIndex;
    [Header("Gun refrences")]
    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    [Header("UI")]
    [SerializeField] GameObject pistolAmmoUI;
    [SerializeField] GameObject shotgunAmmoUI;


    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);
        }
        guns[0].SetActive(true);
        currentGun = guns[0];
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnChangeWeapons(InputValue inputVal)
    {
        
        if (currentWeaponIndex < totalWeapons - 1)
        {
            guns[currentWeaponIndex].SetActive(false);
            currentWeaponIndex++;
            guns[currentWeaponIndex].SetActive(true);
            currentGun = guns[currentWeaponIndex];
             if(currentWeaponIndex >= 2)
        {
                guns[currentWeaponIndex].SetActive(false);

                currentWeaponIndex = 0;
                guns[currentWeaponIndex].SetActive(true); 
        }
        }
       
        
    }
    
}
