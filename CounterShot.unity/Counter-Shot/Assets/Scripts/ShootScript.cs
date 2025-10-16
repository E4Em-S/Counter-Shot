using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class ShootScript : MonoBehaviour
{
    public DiffWeaponsSO weaponData;
     //aiming
    Vector2 lookDir;
    [SerializeField] Transform gunBaseTransform;
    [SerializeField] Transform gunImageTransform;

    [Header("attacking")]
    [SerializeField] float attackRate;
    [SerializeField] GameObject projectile;
    GameObject attackTemp;
    bool attacking = false;
    bool attackRdy = true;
    public int ammo = 0;
    bool isAiming;
    bool isDashing;
    public TextMeshProUGUI ammoCounter;


    public void Update()
    {
         ammoCounter.text = "Ammo: " + ammo;
        if (ammo < 0)
        {
            ammo = 0;
            //add some juice to encourage parrying here
        }
    }
    public void OnLook(InputValue lookValue)
    {
        isAiming = true;
        lookDir = lookValue.Get<Vector2>().normalized;
        if (Mathf.Abs(lookDir.magnitude) > 0)
        {
            gunBaseTransform.up = lookDir;
        }
        if (lookValue.Get<Vector2>() == Vector2.zero) //when there is no more input from right stick (zero vector 2) 
        {
            isAiming = false;
        }
    }
    public void SetWeaponSO(DiffWeaponsSO myWeapons)
    {
        //weapon = myWeapons;
        //projectile = myWeapons.bullet1;
    }
    public void OnFire(InputValue fireValue)
    {
        //Debug.Log("firing");
        if (isAiming == true) //checking that the player is aiming to attack
        {

            attacking = true;
            if (attackRdy && ammo > 0)
            {
                ammo--;
                StartCoroutine(Shoot());
            }
            else attacking = false;
        }
    }

    IEnumerator Shoot()
    {
        if (isAiming == true && isDashing == false) //makes it so the player only can attack when aim is held down &  when not dashing
        {
            attackTemp = Instantiate(projectile, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
            attackTemp.BroadcastMessage("SetDirection", lookDir);
            attackRdy = false;
            yield return new WaitForSeconds(attackRate);
            attackRdy = true;
        }
    }
}
