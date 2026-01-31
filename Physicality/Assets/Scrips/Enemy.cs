using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject player;
    Rigidbody2D rb;
    public float force;
    int parrychance;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int numberOfBullets = 12;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float spawnRadius = 0.5f;
    public Transform bulletpos;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("paddle");
        Vector3 Direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(Direction.x, Direction.y).normalized * force;
        float rot = Mathf.Atan2(-Direction.y, -Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
     
    }

    // Update is called once per frame
    void Update()
    {

    }
 void firebullet()
    {
        Instantiate(bulletPrefab, bulletpos.position, Quaternion.identity);
    }

  IEnumerator waittofire()
    {
       yield return new WaitForSeconds(0.7f);
        firebullet(); 
    }

}

