using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private CowboyCharacterController cowboyCharacterControllerRef;
    [SerializeField] Collider2D playerHitbox;
    public int playerHealth = 3;
    public TextMeshProUGUI healthUI;

    private void Start()
    {
        playerHitbox.enabled = true;
    }
    public void OnTriggerEnter2D(Collider2D other) //checking for damage to player specifically
    {
        playerHealth--;
        {
            if (other.tag == "EnemyProjectile" || other.tag == "Parryable")
        {
                
            Debug.Log("Took damage from: " + other.gameObject);
        }
        }
        
    }
    private void Update()
    {
        healthUI.text = "Health: " + playerHealth;
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
