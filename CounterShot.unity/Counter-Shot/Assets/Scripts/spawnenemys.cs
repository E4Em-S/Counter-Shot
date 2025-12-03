using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnenemys : MonoBehaviour
{
 
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public int numberOfEnemiesToSpawn = 5;

   

    void Start()
    {
        
    }

   public void SpawnEnemies()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No spawn points assigned!");
            return;
        }

        if (numberOfEnemiesToSpawn > spawnPoints.Length)
        {
            Debug.LogWarning("Not enough unique spawn points! Reducing spawn count.");
            numberOfEnemiesToSpawn = spawnPoints.Length;
        }

        // Create a list of available spawn point indices
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            availableIndices.Add(i);
        }

        // Spawn enemies at random unique positions
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, availableIndices.Count);
            int spawnIndex = availableIndices[randomIndex];

            // Spawn the enemy
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);

            // Remove this spawn point from available list 
            
                availableIndices.RemoveAt(randomIndex);
            
        }
    }
}