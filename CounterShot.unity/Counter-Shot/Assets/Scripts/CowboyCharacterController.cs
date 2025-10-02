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
    public int ammo = 5;

    [Header("parrying")]
    public bool isParrying = false;
    [SerializeField] float parryDur; //parry durration
    [SerializeField] CircleCollider2D parryZone;



    [SerializeField] Collider2D playerHitbox;
    public int playerHealth = 3;

    //UI
    public TextMeshProUGUI ammoCounter;

    [Header("flashingColor")]
    public Renderer playerRenderer;
    public Color flashColor = Color.yellow;
    public float flashDur = 0.1f;
    private Color ogColor;
    public float freezeDur;
    bool isFrozen = false;

    void Awake()
    {
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
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

        ammo--;
        attacking = true;
        if (attackRdy && ammo > 0)
        {
            StartCoroutine(Shoot());

        }
        else attacking = false;
    }
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
                StartCoroutine(DoFreeze());
                //StartCoroutine(PoopieChudWaitForSec());
                
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
        playerRenderer.material.color = flashColor;
        yield return new WaitForSecondsRealtime(parryDur);
        playerRenderer.material.color = ogColor;
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
   
}


