using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.EventSystems;
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
    public int ammo = 0;

    [Header("parrying")]
    public bool isParrying = false;
    [SerializeField] float parryDur; //parry durration
    [SerializeField] CircleCollider2D parryZone;

    [Header("PlayerHealth")]
    [SerializeField] Collider2D playerHitbox;
    public int playerHealth = 3;

    [Header("Dash")]
    public bool isDashing;
    bool canDash;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDur = 1f;
    [SerializeField] float dashCooldown = 1f;

    //UI
    public TextMeshProUGUI ammoCounter;

    [Header("flashingColor")]
    public Renderer playerRenderer;
    public Color flashColor = Color.yellow;
    public float flashDur = 0.1f;
    private Color ogColor;
    public float freezeDur;
    bool isFrozen = false;



    bool isAiming; //check if the right stick is recieving input
    void Awake()
    {
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
        SetWeaponSO(weapon);
        playerHitbox.enabled = true;
        parryZone.enabled = false;
        playerRenderer = GetComponent<Renderer>();
        ogColor = playerRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        ammoCounter.text = "Ammo: " + ammo;
        if (ammo < 0)
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
        if(isDashing == false)
        {
            movementInput = value.Get<Vector2>();
        }
    }
   /* public void OnLook(InputValue lookValue)
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
    }*/
    public void OnDash(InputValue dashValue)
    {
        // if (isAiming == true)
        {
            StartCoroutine(Dash());
        }
    }
  /*  public void OnFire(InputValue fireValue)
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
    }*/
    public void OnParry(InputValue parryValue)
    {
        isParrying = true;
        StartCoroutine(Parry());
    }
    public void SetWeaponSO(DiffWeaponsSO myWeapons)
    {
        weapon = myWeapons;
        //projectile = myWeapons.bullet1;
    }
  /*  IEnumerator Shoot()
    {
        if (isAiming == true && isDashing == false) //makes it so the player only can attack when aim is held down &  when not dashing
        {
            attackTemp = Instantiate(projectile, gunImageTransform.position, Quaternion.LookRotation(lookDir)); //Quaternion.identity no rot
            attackTemp.BroadcastMessage("SetDirection", lookDir);
            attackRdy = false;
            yield return new WaitForSeconds(attackRate);
            attackRdy = true;
        }

    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isParrying == true)
        {
            if (collision.tag == "Parryable")
            {
                StartCoroutine(DoFreeze());
                ammo++;
                //Debug.Log("parry collision w: " + collision.gameObject);

            }//come up with full ammo system when doing other weapons and change this bullshit
            if (ammo > 6)
            {
                ammo = 6;
            }
        }
    }

    IEnumerator Parry() //creates a delay, so the player can't parrry every sec
    {
        parryZone.enabled = true;
        //playerRenderer.material.color = flashColor;
        yield return new WaitForSecondsRealtime(parryDur);
        // playerRenderer.material.color = ogColor;
        parryZone.enabled = false;
        isParrying = false;

    }
    IEnumerator DoFreeze()
    {
        isFrozen = true;
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(freezeDur);
        Time.timeScale = 1f;
        isFrozen = false;
    }
    private IEnumerator Dash()
    {
        //attackTemp.BroadcastMessage("SetDirection", lookDir);


        canDash = false;
        isDashing = true;
       // movementInput = new Vector2(lookDir.x * dashSpeed, lookDir.y * dashSpeed);
        movementInput = new Vector2(movementInput.x * dashSpeed, movementInput.y * dashSpeed);
       // rb.velocity = new Vector2(movementInput.x * dashSpeed, movementInput.y * dashSpeed);
        // rb.velocity = new Vector2(lookDir.x * dashSpeed, lookDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDur);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
  
}


