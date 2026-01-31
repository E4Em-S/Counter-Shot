using UnityEngine;

public class Bouncy_Ball : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float minYvelocity;
    [SerializeField] float minXvelocity;
    Rigidbody2D rb;
    bool gamestarted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
     
    }

    void Update()
    {
        if (!gamestarted && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchBall();
        }
        if (gamestarted)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * speed;
            preventsoftlock();
        }
    }

    void LaunchBall()
    {
        gamestarted = true;
        //launches the ball at a random upward angle
        float randomx = Random.Range(-0.5f, 0.5f);
        Vector2 launchdir = new Vector2(randomx, 1f).normalized;
        rb.linearVelocity = launchdir * speed;
    }

    void preventsoftlock()
    {
        Vector2 velocity = rb.linearVelocity;
        bool changed = false;
        //prevents ball from getting stuck only moving horizontal
        if (Mathf.Abs(velocity.y) < minYvelocity)
        {
            float newY = velocity.y > 0 ? minYvelocity : -minYvelocity;
            velocity.y = newY;
            changed = true;
        }
        // Prevent too vertical (stuck going up and down)
        if (Mathf.Abs(velocity.x) < minXvelocity)
        {
            float newX = velocity.x > 0 ? minXvelocity : -minXvelocity;
            velocity.x = newX;
            changed = true;
        }
        if (changed)
        {
            rb.linearVelocity = velocity.normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
            //getting the bounds of the paddle
            Collider2D paddlecollider = collision.collider;
            float paddleWidth = paddlecollider.bounds.size.x;
            //Calculate where the ball hits the paddle
            float hitposition = (transform.position.x - collision.transform.position.x) / (paddleWidth / 2f);
            //prevent extreme angles
            hitposition = Mathf.Clamp(hitposition, -0.8f, 0.8f);
            //create new velocity 
            float horizontalStrength = hitposition * 2f;
            Vector2 newDirection = new Vector2(horizontalStrength, 1f).normalized;
            rb.linearVelocity = newDirection * speed;
        }
       
    }
}

