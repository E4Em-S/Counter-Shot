using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
public class CowboyCharacterController : MonoBehaviour
{
    DiffWeaponsSO weapon;
    //movement
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movementInput;

    [Header("changeables")]
    [SerializeField] public float moveSpeed;


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
    public int ammo = 6;

    [Header("parrying")]
    public bool isParrying = false;
    [SerializeField] int pps; //pps
    [SerializeField] Collider2D parryZone;


    //GameObject parryZone;

    //UI
    public TextMeshProUGUI ammoCounter;


    // Start is called before the first frame update
    void Start()
    {
        SetWeaponSO(weapon);
        //parryZone.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = "Ammo: " + ammo;
        if (ammo <= 0)
        {
            ammo = 0;
            //add some juice to encourage parrying here
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
    public void OnLook(InputValue lookValue)
    {
        lookDir = lookValue.Get<Vector2>().normalized;
        if (Mathf.Abs(lookDir.magnitude) > 0)
        {
            gunBaseTransform.up = lookDir;
        }
        else
        {
            //gunBaseTransform.up = lookDir;
        }
    }
    public void OnFire(InputValue fireValue)
    {
        //still need to figure out something to prevent it from firing when not aiming
        //if (lookDir.Get<Vectgor2>().normalized > 0)//if(Mathf.Abs(fireValue.Get<float>()) > 0)
        // {
        ammo--;
        attacking = true;
        if (attackRdy && ammo > 0)
        {
            StartCoroutine(Shoot());
            // }
        }
        else attacking = false;
    }
    public void OnParry(InputValue parryValue)
    {
        //Debug.Log("parrying");
        isParrying = true;
        StartCoroutine(Parry());
        //parryZone.enabled = true;
    }
    public void SetWeaponSO(DiffWeaponsSO myWeapons)
    {
        weapon = myWeapons;
        //projectile = myWeapons.bullet1;
    }
    IEnumerator Shoot()
    {
        attackTemp = Instantiate(projectile, gunImageTransform.position, Quaternion.identity);
        attackTemp.BroadcastMessage("SetDirection", lookDir);
        attackRdy = false;
        yield return new WaitForSeconds(attackRate);
        attackRdy = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isParrying == true)
        {
            if (collision.tag == "Parryable")
            {
                ammo++;
                Debug.Log("parried: " + collision.gameObject);
                //Destroy(collision.gameObject);
            }
            if (ammo > 6)
            {
                ammo = 6;
            }
        }
    }
    IEnumerator Parry()
    {
        parryZone.enabled = true;
        yield return new WaitForSeconds(pps);
        parryZone.enabled = false;
        isParrying = false;
    }
}


