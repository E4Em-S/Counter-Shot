using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class ShootScript : MonoBehaviour
{

    [SerializeField] private CowboyCharacterController cowboyCharacterControllerRef;
    public WeaponChanger weaponManager;
    public DiffWeaponsSO weaponData;
    //aiming
    Vector2 lookDir;
    [SerializeField] Transform gunBaseTransform;
    [SerializeField] Transform gunImageTransform;

    [Header("attacking")]
    [SerializeField] float attackRate;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject shotgunBullet;
    GameObject attackTemp;
    bool attacking = false;
    bool attackRdy = true;
    public int ammo = 0;
    bool isAiming;
    bool isDashing;
    public TextMeshProUGUI ammoCounter;
    [Header("Shotgun")]
    public float spreadAngle = 5f;
    public float shotgunSpeed;

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
            if (weaponManager.currentWeaponIndex == 0) //pistol shooting
            {
                attackTemp = Instantiate(projectile, gunImageTransform.position, Quaternion.identity); //Quaternion.identity no rot
                attackTemp.BroadcastMessage("SetDirection", lookDir);
                attackRdy = false;
                yield return new WaitForSeconds(attackRate);
                attackRdy = true;
            }
             if(weaponManager.currentWeaponIndex == 1) //shotgun shooting
            {
                //Debug.Log("Shotgun Shot");
               /* for (int i = 0; i < 3; i++)
                {
                    Vector3 spreadDirection = gunImageTransform.forward;
                    float randomAngleX = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                    float randomAngleY = Random.Range(-spreadAngle / 2f, spreadAngle / 2f);
                    spreadDirection = Quaternion.Euler(randomAngleX, randomAngleY, 0) * spreadDirection;
                    attackTemp = Instantiate(projectile, gunImageTransform.position, gunImageTransform.rotation);
                   // attackTemp.BroadcastMessage("SetDirection", lookDir);

                    Rigidbody2D rb = attackTemp.GetComponent<Rigidbody2D>();
                    rb.AddForce(spreadDirection.normalized * shotgunSpeed, ForceMode2D.Impulse);

                }*/
                
                  for (int i = 0; i <= 2; i++)
                  {
                      switch (i)
                      {
                        case 0:
                          attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir1 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir1 * shotgunSpeed);
                            //attackTemp.transform.Rotate(90, 0, 0);
                            //zxy

                              break;
                          case 1:
                              attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir2 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir2 * shotgunSpeed);
                            //attackTemp.transform.Rotate(45, 0, 0);
                              
                              break;
                          case 2:
                              attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir3 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir3 * shotgunSpeed);
                            //attackTemp.transform.Rotate(-45, 0, 0);
                              //attackTemp.transform.Rotate(Vector3.up * (Random.Range(-spreadAngle / 2f, spreadAngle / 2f)));
                              break;
                      }
                      attackRdy = false;
                      yield return new WaitForSeconds(attackRate);
                      attackRdy = true;


                  }
              }

          }
            }
        }
    

