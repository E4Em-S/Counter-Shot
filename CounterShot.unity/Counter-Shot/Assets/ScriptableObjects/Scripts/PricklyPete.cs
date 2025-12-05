using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PricklyPete : MonoBehaviour
{
    public GameObject spikeball;
    public Transform spikeballPos;
    public EnemyHealth enemyHealth;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
  public void fireSpikeBall()
    {
        Instantiate(spikeball, spikeballPos.position, Quaternion.identity);
    }
}
