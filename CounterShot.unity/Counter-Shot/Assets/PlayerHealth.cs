using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private CowboyCharacterController cowboyCharacterControllerRef;
    [SerializeField] Collider2D playerHitbox;
    public int playerHealth = 3;

    private void Start()
    {
        playerHitbox.enabled = true;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //if (cowboyCharacterControllerRef.isParrying == false)
        {
            if (other.tag == "EnemyProjectile" || other.tag == "Parryable")
        {
            Debug.Log("Took damage from: " + other.gameObject);
        }
        }
        
    }
    public void OnParry()
    {
        
        //playerHitbox.enabled = false;
    }
}
