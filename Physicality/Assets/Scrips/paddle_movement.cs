using UnityEngine;

public class paddle_movement : MonoBehaviour
{
    public float speed;
    float horizontalmovement;
    [SerializeField] float maxX;
    Rigidbody2D rb;
    public Player_Health playerhp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalmovement = Input.GetAxis("Horizontal");
        if ((horizontalmovement > 0 && transform.position.x < maxX) || (horizontalmovement < 0 && transform.position.x > -maxX))
        {

            transform.position += Vector3.right * horizontalmovement * speed * Time.deltaTime;
        }



    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("paddle"))
        {
            //getting the bounds of the paddle
            Collider2D paddlecollider = collision.collider;
            float paddleWidth = paddlecollider.bounds.size.x;

            //Calcuate where the ball hits the paddle
            float hitposition = (transform.position.x - collision.transform.position.x) / (paddleWidth / 2f);
            //prevent extreme angles
            hitposition = Mathf.Clamp(hitposition, -0.8f, 0.8f);

            //create new velocity with strong horizontal component
            float horizontalStrength = hitposition * 2f;
            Vector2 newDirection = new Vector2(horizontalStrength, 1f).normalized;

            rb.linearVelocity = newDirection * speed;



        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            Destroy(other.gameObject);
            playerhp.TakeDamage(1);
        }
    }
}
