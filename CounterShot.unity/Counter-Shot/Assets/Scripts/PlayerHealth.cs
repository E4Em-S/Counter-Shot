using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private CowboyCharacterController cowboyCharacterControllerRef;
    [SerializeField] Collider2D playerHitbox;
    public int playerHealth = 3;
    public TextMeshProUGUI healthUI;

    [Header("taking dam")]
    [SerializeField] SpriteRenderer playerRenderer;
    public Color flashColor = Color.red;
    public float flashDur = 0.2f;

    private Color ogColor;

    private int maxPlayerHealth = 3;
    public Slider cigaretteSlider;

    private void Start()
    {
        playerHitbox.enabled = true;
        ogColor = playerRenderer.material.color;
        cigaretteSlider.maxValue = maxPlayerHealth;
        cigaretteSlider.maxValue = playerHealth;
    }
    public void OnTriggerEnter2D(Collider2D other) //checking for damage to player specifically
    {
        
        if (other.tag == "EnemyProjectile" || other.tag == "Parryable" || other.tag == "Horse")
        {
            if (cowboyCharacterControllerRef.isDashing == true || cowboyCharacterControllerRef.isInvincible == true)//check if dashing
                return;
            playerHealth--;
            cigaretteSlider.value = playerHealth;
            StartCoroutine(FlashRed());
            //Debug.Log("Took damage from: " + other.gameObject);
        }
    }
    private void Update()
    {
        healthUI.text = "Health: " + playerHealth;
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(3);
        }
    }
       public void flashRed()
    {
        StopAllCoroutines();
        StartCoroutine(FlashRed());
    }
    IEnumerator FlashRed()
    {
        playerRenderer.material.color = flashColor;
        yield return new WaitForSeconds(flashDur);
        playerRenderer.material.color = ogColor;
    }
}
