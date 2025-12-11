using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikeball : MonoBehaviour
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
    [SerializeField] Sprite parryBulletImg;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 Direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(Direction.x, Direction.y).normalized * force;
        float rot = Mathf.Atan2(-Direction.y, -Direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot);
        StartCoroutine(waittoexplode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    
    IEnumerator waittoexplode()
    {
        yield return new WaitForSeconds(.75f);
        float angleStep = 360f / numberOfBullets;

        for (int i = 0; i < numberOfBullets; i++)
        {
            // Calculate angle in degrees and convert to radians
            float angle = i * angleStep;
            float angleRad = angle * Mathf.Deg2Rad;

            // Calculate spawn position
            Vector2 spawnOffset = new Vector2(
                Mathf.Cos(angleRad) * spawnRadius,
                Mathf.Sin(angleRad) * spawnRadius
            );
            Vector2 spawnPosition = (Vector2)transform.position + spawnOffset;

            // Spawn bullet
            parrychance = Random.Range(0, 5);

            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
           //changes the bullet to be parriable
            if (parrychance == 3 || parrychance == 4)
            {
                SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();
                sprite.color = new UnityEngine.Color(1f, 0f, 0.82f);
                bullet.tag = "Parryable";
                Parryablebullet parryScript = bullet.AddComponent<Parryablebullet>();

                parryScript.parrybulletsprite = parryBulletImg;
                //bullet.AddComponent<Parryablebullet>();

            }

            // Calculate direction (normalized vector pointing outward)
            Vector2 direction = spawnOffset.normalized;

            // Apply velocity if bullet has Rigidbody2D
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }

            //rotate bullet
            float bulletAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, bulletAngle);
            Destroy(gameObject);
        }
    }
}
