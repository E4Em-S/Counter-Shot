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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        parryexplode = GetComponent<Animator>();
        //rb.velocity = transform.position * speed;
        cd = GetComponent<CircleCollider2D>();
        cc = GetComponent<CapsuleCollider2D>();
        StartCoroutine(Destroyself());
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
            parryexplode.SetTrigger("isParried");
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
