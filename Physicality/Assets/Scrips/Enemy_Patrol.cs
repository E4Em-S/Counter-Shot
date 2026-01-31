using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    Rigidbody2D rb;
    Transform currentpoint;
    [SerializeField] float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentpoint = PointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentpoint.position - transform.position;
        if (currentpoint == PointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentpoint.position) < 0.5f )
        {
           currentpoint = (currentpoint == PointB.transform) ? PointA.transform : PointB.transform;
        }
      
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
    }
}
