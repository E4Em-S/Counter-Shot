using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("attacking/damage")]
    public int damage;
    Rigidbody2D rb;
    public float bulletSpeed;
    public bool destroyOnContact = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void SetDirection(Vector2 myDir)
    {
        transform.up = myDir;
        if (rb != null)
            rb.velocity = myDir.normalized * bulletSpeed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            Debug.Log("Damaging");
            //collision.gameObject.GetComponent // put refrence to enemy script here //.AddDamage(damage)
            if(destroyOnContact) Destroy(gameObject);
        }
    }
}
