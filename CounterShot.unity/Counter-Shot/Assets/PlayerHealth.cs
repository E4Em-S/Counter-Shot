using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int playerHealth = 3;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyProjectile" || other.tag == "Parryable")
        {
            Debug.Log("Collided w: " + other.gameObject);
            Debug.Log("Player taking damage");
        }
    }
}
