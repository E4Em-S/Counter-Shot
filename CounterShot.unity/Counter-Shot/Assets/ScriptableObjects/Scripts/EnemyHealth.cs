using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;
    public int maxHealth;
    // Start is called before the first frame update
    public TrainingDummyRespawnManager trainingDummyRespawnManagerScript;
     [SerializeField] public Scene currentScene;


    [Header("LayoutGroup")]
    public LayoutGroup targetLayoutGroup;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (SceneManager.GetActiveScene().name == "TutorialScene")
            trainingDummyRespawnManagerScript = GameObject.Find("trainingDummyrespawnManager").GetComponent<TrainingDummyRespawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            if(SceneManager.GetActiveScene().name == "SundanceFight")
            {
                SceneManager.LoadScene(4);
            }
            
            if(SceneManager.GetActiveScene().name == "TutorialScene")
            {
                Debug.Log("SceneMatches");
                trainingDummyRespawnManagerScript.Invoke("RespawnTrainingDummy", 1);
            }
            
            //currentHealth = maxHealth;
            Destroy(gameObject);
        }
    }
    //wronggggg
    public void AddDamage(int damage)
    {
        currentHealth = currentHealth - damage;
    }
    


    }
