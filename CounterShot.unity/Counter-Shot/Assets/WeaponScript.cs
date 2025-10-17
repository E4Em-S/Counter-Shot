using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class WeaponScript : MonoBehaviour
{
    int totalWeapons;

    public int currentWeaponIndex;
    [Header("Gun refrences")]
    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    // Start is called before the first frame update
    void Start()
    {
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
        //Debug.Log("Changing weapons");
        if (currentWeaponIndex < totalWeapons - 1)
        {
            guns[currentWeaponIndex].SetActive(false);
            currentWeaponIndex++;
            guns[currentWeaponIndex].SetActive(true);
            currentGun = guns[currentWeaponIndex];
             if(currentWeaponIndex >= 2)
        {
            currentWeaponIndex = 0; 
        }
        }
       
        
    }
    
}
