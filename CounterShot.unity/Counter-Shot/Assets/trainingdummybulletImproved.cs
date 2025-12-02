using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingdummybulletImproved : MonoBehaviour
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
   
}
