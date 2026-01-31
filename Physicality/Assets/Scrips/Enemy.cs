using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    //how many shots per second
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletSpeed = 10f;

    private Transform player;
    private float nextFireTime;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("paddle")?.transform;

       
    }

    private void Update()
    {
        if (player == null) return;

      

       
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

  

    private void Shoot()
    {
       
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.linearVelocity = direction * bulletSpeed;
        }
    }
}

