using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;


public class ShootScript : MonoBehaviour
{
    public WeaponChanger weaponManager;
    public DiffWeaponsSO weaponData;
    //ui
    public TextMeshProUGUI ammoCounter;
    //aiming
    Vector2 lookDir;
    [SerializeField] Transform gunBaseTransform;
    [SerializeField] Transform gunImageTransform;

    [Header("pistol")]
    [SerializeField] Animator reloadPistol;
    [SerializeField] float pistolattackRate;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject pistolAmmoUI;
    int bulletspistol;
    public AudioSource pistolShoot;

    GameObject attackTemp;
    bool attacking = false;
    bool attackRdy = true;

    [SerializeField] int pistolAmmo;
    bool isAiming;
    bool isDashing;
    [Header("Shotgun")]
     [SerializeField] GameObject shotgunAmmountUI;
    [SerializeField] GameObject shotgunBullet;
    public float spreadAngle = 14f;
    public float shotgunSpeed;
    public int shotgunAmmo;
    public float shotgunAttackRate;
    int bulletsShotgun;
    Animator gunFire;
    public GameObject sixShooter;
    public AudioSource shotgunShoot;

    public void Update()
    {
        //UpdateAmmoDisplay();

        if (pistolAmmo < 0 || shotgunAmmo < 0)
        {
            //add some juice to encourage parrying here
        }
    }
    void Start()
    {
        gunFire = sixShooter.GetComponent<Animator>();
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
    void OnChangeWeapons(InputValue inputval)
    {
        UpdateAmmoDisplay();
    }
    public void SetWeaponSO(DiffWeaponsSO myWeapons)
    {
        //weapon = myWeapons;
        //projectile = myWeapons.bullet1;
    }
    public void OnFire(InputValue fireValue)
    {
        if(pistolAmmo > 0)
        {
          //gunFire.SetTrigger("Fire");  
        }
        

        //Debug.Log("firing");
        if (isAiming == true) //checking that the player is aiming to attack
        {
            attacking = true;
            if (weaponManager.currentWeaponIndex == 0 && attackRdy && pistolAmmo > 0)
            {
                RemoveBulletPistolUI();
                pistolShoot.Play();
                pistolAmmo--;
                StartCoroutine(Shoot());
            }
            else if (weaponManager.currentWeaponIndex == 1 && attackRdy && shotgunAmmo > 0)
            {
                RemoveBulletShotgunUI();
                shotgunShoot.Play();
                shotgunAmmo--;
                StartCoroutine(Shoot());
            }
            else attacking = false;
            UpdateAmmoDisplay();
        }
    }
    public void UpdateAmmo()
    {
       switch (weaponManager.currentWeaponIndex)
        {
            case 0:
                if (pistolAmmo >= 6) break;
                pistolAmmo++;
                pistolAmmo++;
                //reloadPistol.SetTrigger("Spin");
                AddBulletPistolUI();
                AddBulletPistolUI();
            break;
            case 1:
                if (shotgunAmmo >= 4) break;
                AddShotgunBulletUI();
                shotgunAmmo++;
            break;
        }     
        UpdateAmmoDisplay();
    }
    void UpdateAmmoDisplay()
    {
        switch (weaponManager.currentWeaponIndex)
        {
            case 0:
                pistolAmmoUI.SetActive(true);
                shotgunAmmountUI.SetActive(false);
                ammoCounter.text = "Ammo (pistol): " + pistolAmmo;
                break;
            case 1:
                pistolAmmoUI.SetActive(false);
                shotgunAmmountUI.SetActive(true);
                ammoCounter.text = "Ammo (shotgun): " + shotgunAmmo;
                break;
        }
    }
    public void RemoveBulletPistolUI()
    {
       
        bulletspistol = pistolAmmoUI.transform.childCount;
        for (int i = bulletspistol - 1; i >= 0 - 1; i--)
        {
            GameObject bullet = pistolAmmoUI.transform.GetChild(i).gameObject;
            //Debug.Log(i);
            if (bullet.activeSelf)
            {
                bullet.SetActive(false);
                break;
            }
        }
    }
    public void AddBulletPistolUI()
    {
        //pistolAmmoUI.BroadcastMessage("onRotate");
        bulletspistol = pistolAmmoUI.transform.childCount;
        for (int i = bulletspistol - 1; i >= 0 - 1; i--)
        {
            GameObject bullet = pistolAmmoUI.transform.GetChild(i).gameObject;
            //Debug.Log(i);
            if (!bullet.activeSelf)
            {
                bullet.SetActive(true);
                break;
            }
        }
    }
    public void AddShotgunBulletUI()
    {
        bulletsShotgun = shotgunAmmountUI.transform.childCount;
        for (int i = bulletsShotgun - 1; i >= 0; i--)
        {
            if (i > 1) 
            break;
            GameObject bullet = shotgunAmmountUI.transform.GetChild(i).gameObject;
            //Debug.Log(i);
            
            if (!bullet.activeSelf)
            {
                bullet.SetActive(true);
                break;
            }
        }
    }
    public void RemoveBulletShotgunUI()
    {
       
        bulletsShotgun = shotgunAmmountUI.transform.childCount;
        for (int i = bulletsShotgun - 1; i >= 0 - 1; i--)
        {
            GameObject bullet = shotgunAmmountUI.transform.GetChild(i).gameObject;
            
            if (bullet.activeSelf)
            {
                bullet.SetActive(false);
                break;
            }
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
                yield return new WaitForSeconds(pistolattackRate);
                attackRdy = true;
            }
             if(weaponManager.currentWeaponIndex == 1) //shotgun shooting
            {
                for (int i = 0; i <= 4; i++)
                {
                    switch (i)
                    {
                        case 0:
                            attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir1 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir1 * shotgunSpeed);
                            break;
                        case 1:
                            attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir2 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir2 * shotgunSpeed);
                            break;
                        case 2:
                            attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir3 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir3 * shotgunSpeed);
                            break;
                        case 3:
                            attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir4 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir4 * shotgunSpeed);
                            break;
                        case 4:
                            attackTemp = Instantiate(shotgunBullet, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
                            attackTemp.BroadcastMessage("SetDirection", lookDir);
                            Vector3 dir5 = transform.forward + new Vector3(Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle), Random.Range(-spreadAngle, spreadAngle));
                            attackTemp.GetComponent<Rigidbody2D>().AddForce(dir5 * shotgunSpeed);
                            break;
                    }
                    attackRdy = false;
                    attackRdy = true;
                }
                yield return new WaitForSeconds(shotgunAttackRate);
            }

          }
            }
        }
    

