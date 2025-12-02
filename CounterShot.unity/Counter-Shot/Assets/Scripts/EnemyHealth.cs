using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public int maxHealth;
    // Start is called before the first frame update
    [SerializeField] GameObject trainingDummyPrefab;
    [SerializeField] Transform trainingdummyposition;


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
            RespawnTrainingDummy();
            Destroy(gameObject);
        }
    }
    //wronggggg
    public void AddDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }
    public IEnumerator RespawnTrainingDummy(){
        yield return new WaitForSeconds(1);
        GameObject.Instantiate(trainingDummyPrefab, trainingdummyposition.position, Quaternion.identity);
        Debug.Log("spawn another");
    }
    }
