using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parryablebullet : MonoBehaviour
{
    public float speed = 8;
    Rigidbody2D rb;
    CircleCollider2D cd;
    CapsuleCollider2D cc;
    [SerializeField] Animator parryexplode;
    public Sprite parrybulletsprite;

    // Start is called before the first frame update
    void Start()
    {
       


     /*   parryexplode = this.GetComponent<Animator>();
        if(parryexplode == null)
        {
            parryexplode = this.AddComponent<Animator>();
        }*/
        rb = GetComponent<Rigidbody2D>();
        parryexplode = GetComponent<Animator>();
        //rb.velocity = transform.position * speed;
        cd = GetComponent<CircleCollider2D>();
        cc = GetComponent<CapsuleCollider2D>();
        StartCoroutine(Destroyself());

        if (parrybulletsprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = parrybulletsprite;
        }
        parryexplode = GetComponent <Animator>();
        if(parryexplode != null)
        {
            parryexplode.enabled = false; 
        }
        else
        {
            parryexplode = this.GetComponent <Animator>();
            if(parryexplode == null)
            {
                parryexplode= this.AddComponent<Animator>();
            }
        }

        //parryexplode.enabled = false;
        //SpriteRenderer sr = GetComponent<SpriteRenderer>();
        //sr.sprite = parrybulletsprite;
    }

    IEnumerator Destroyself()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision is we parring animatoring");
        if (collision.tag == "parryCircle")
        {
            //Debug.Log(collision);
            parryexplode.enabled = true;
            //parryexplode.SetTrigger("isParried");
            Destroy(gameObject); // if animation ist playing in main scene its because of this, making ti so that the bullet destroys in training scene
        }
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
    public void OnAnimStart()
    {

        rb.velocity = transform.position * 0;
        if(cc == null) cd.enabled = false; //circle colider
        if(cd == null) cc.enabled = false; //capsule collider
        
    }
    public void OnAnimFinishDestroy()
    {
        //StartCoroutine(PoopieChudWaitForSec());
        Destroy(gameObject);
    }
}
