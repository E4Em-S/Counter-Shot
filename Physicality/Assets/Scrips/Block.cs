using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Block : MonoBehaviour
{
    Vector2 originalPosition;
    Rigidbody2D rb;
    bool isfalling = false;
    public float respawnDelay = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("ball") && !isfalling)
        {
            Fall();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void Fall()
    {
        isfalling = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        //respawns the block on a timer
        Invoke("Respawn", respawnDelay);

    }
    void Respawn()
    {
        //reset position and physics
        transform.position = originalPosition;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        transform.rotation = Quaternion.identity;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = false;
        }
        isfalling = false;



    }
}
