using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinningBullet : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public static bool destroyOnContact = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (destroyOnContact) Destroy(gameObject);
    }
}

