using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] int maxHealth;
    // Start is called before the first frame update


    [Header("LayoutGroup")]
    public LayoutGroup targetLayoutGroup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void AddDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }
    }
