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
        transform.right = myDir;
        if (rb != null)
            rb.velocity = myDir.normalized * bulletSpeed;
    }


    //something that checks what weapon player has & how much damage it does 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().AddDamage(damage);
            collision.gameObject.GetComponent<EnemyHealthBar>().UpdateHealthBar(damage);
            //Debug.Log("Doing: " + Mathf.RoundToInt(damage) + "damage");
            if (destroyOnContact) Destroy(gameObject);
        }
    }
   
}
