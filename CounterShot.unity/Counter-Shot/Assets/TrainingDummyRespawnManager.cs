using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummyRespawnManager : MonoBehaviour
{
    [SerializeField] GameObject trainingdummyprefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnTrainingDummy()
    {

        Instantiate(trainingdummyprefab);
    }
}
