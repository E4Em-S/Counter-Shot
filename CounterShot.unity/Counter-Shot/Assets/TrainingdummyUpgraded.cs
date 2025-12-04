using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingdummyUpgraded : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int numberOfBullets = 12;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float spawnRadius = 0.5f;
    [SerializeField] Sprite parryBulletImg;
    int parrychance;

    [SerializeField] GameObject trainingDummyPrefab;
    [SerializeField] Transform trainingdummyposition;

    public EnemyHealth enemyHealthScript;

    void Start()
    {

        StartCoroutine(waittoexplode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    IEnumerator waittoexplode()
    {
        yield return new WaitForSeconds(2.6f);
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
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            parrychance = Random.Range(0, 5);
            if (parrychance == 3 || parrychance == 4)
            {
                SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();
               
                bullet.tag = "Parryable";

                Parryablebullet parryScript = bullet.AddComponent<Parryablebullet>();
                parryScript.parrybulletsprite = parryBulletImg;
                bullet.AddComponent<Parryablebullet>(); //adds parryable bullet script to the obj
                
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
        }
        playCorroutineAgain();
    }
    public void playCorroutineAgain()
    {
        StartCoroutine(waittoexplode());
    }
  
}

