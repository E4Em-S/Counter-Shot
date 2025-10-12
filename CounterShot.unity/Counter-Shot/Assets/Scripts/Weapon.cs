using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Weapon : MonoBehaviour
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


    public void SetWeaponSO(DiffWeaponsSO myWeapons)
    {
        //weapon = myWeapons;
        //projectile = myWeapons.bullet1;
    }
    public void OnFire(InputValue fireValue)
    {
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
