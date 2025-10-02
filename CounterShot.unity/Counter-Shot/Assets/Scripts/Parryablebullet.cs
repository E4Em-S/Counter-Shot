using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Parryablebullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    CircleCollider2D cd;
    [SerializeField] Animator parryexplode;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.position * speed;
        cd = GetComponent<CircleCollider2D>();
        StartCoroutine(Destroyself());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Destroyself()
    {
        yield return new WaitForSeconds(6);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "parryCircle")
        {
            Debug.Log(collision);
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
        cd.enabled = false;
    }
    public void OnAnimFinishDestroy()
    {
        //StartCoroutine(PoopieChudWaitForSec());
        Destroy(gameObject);
    }
     IEnumerator PoopieChudWaitForSec()
    {
        yield return new WaitForSeconds(2f);
    }
}
