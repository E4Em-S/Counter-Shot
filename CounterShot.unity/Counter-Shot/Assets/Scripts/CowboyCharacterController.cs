using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class CowboyCharacterController : MonoBehaviour
{
    DiffWeaponsSO weapon;
    [SerializeField] private ShootScript shootScript;
    //movement
    [SerializeField] private Rigidbody2D rb;
    private Vector2 movementInput;

    [Header("changeables")]
    [SerializeField] public float moveSpeed;

    //aiming
    Vector2 lookDir;
    [SerializeField] Transform gunBaseTransform;
    [SerializeField] Transform gunImageTransform;

    [Header("parrying")]
    public bool isParrying = false;
    [SerializeField] float parryDur; //parry durration
    [SerializeField] CircleCollider2D parryZone;
    public ParticleSystem parryfogparticle;
    Animator parryanim;
    [SerializeField] AudioSource parryAudioSource;

    [Header("PlayerHealth")]
    [SerializeField] Collider2D playerHitbox;
    public bool isInvincible;
    public int playerHealth = 3;


    [Header("Dash")]
    public bool isDashing;
    bool canDash;
    [SerializeField] float dashSpeed = 10f;
    [SerializeField] float dashDur = 1f;
    [SerializeField] float dashCooldown = 1f;

    //UI


    [Header("flashingColor")]
    public Renderer playerRenderer;
    private Color ogColor;
    public float freezeDur;
    bool isFrozen = false;



    bool isAiming; //check if the right stick is recieving input
    void Awake()
    {
        Time.timeScale = 1;
        parryfogparticle = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if(movementInput.x == 0 && movementInput.y == 0)
        {
            //parryanim.SetTrigger("BacktoIdle");
            parryanim.SetBool("isMoving", false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        parryanim = GetComponent<Animator>();
        canDash = true;
        SetWeaponSO(weapon);
        playerHitbox.enabled = true;
        parryZone.enabled = false;
        playerRenderer = GetComponent<Renderer>();
        ogColor = playerRenderer.material.color;

    }

    private void FixedUpdate()
    {
        rb.velocity = movementInput * moveSpeed;
    }

    public void OnMove(InputValue value)
    {
        parryanim.SetFloat("inputX", movementInput.x);
        parryanim.SetFloat("inputY", movementInput.y);
        parryanim.SetBool("isMoving", true);

        if (isDashing == false)
        {
            movementInput = value.Get<Vector2>();

        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            parryanim.SetFloat("LastInputX", movementInput.x);
            parryanim.SetFloat("LastInputY", movementInput.y);
            parryanim.SetBool("isMoving", false);
        }
    }

    public void OnDash(InputValue dashValue)
    {
        {
            StartCoroutine(Dash());
        }
    }
    

    
    public void OnParry(InputValue parryValue)
    {
        isParrying = true;
        
        if (movementInput.x > 0)
        {
            parryanim.SetTrigger("ParryRight");
        }
        else
        {
            parryanim.SetTrigger("ParryLeft");

        }
        
        StartCoroutine(Parry());
    }
    public void SetWeaponSO(DiffWeaponsSO myWeapons)
    {
        weapon = myWeapons;
        //projectile = myWeapons.bullet1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isParrying == true)
        {
            if (collision.tag == "Parryable")
            {
                parryAudioSource.Play();
                parryfogparticle.Play();
                playerHitbox.enabled = false;
                StartCoroutine(DoFreeze());
                GetComponent<ShootScript>().UpdateAmmo();
                playerHitbox.enabled = true;
                //parryfogparticle.Stop();
                //Debug.Log("parry collision w: " + collision.gameObject);

            }
        }
    }

    IEnumerator Parry() //creates a delay, so the player can't parrry every sec
    {
        parryZone.enabled = true;
        isInvincible = true;
        //playerRenderer.material.color = flashColor;
        yield return new WaitForSecondsRealtime(parryDur);
        // playerRenderer.material.color = ogColor;
        parryZone.enabled = false;
        isInvincible = false;
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
        canDash = false;
        isDashing = true;
        movementInput = new Vector2(movementInput.x * dashSpeed, movementInput.y * dashSpeed);
        yield return new WaitForSeconds(dashDur);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
  
}


