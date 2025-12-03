using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pricklypetecallfriends : StateMachineBehaviour
{
    GameObject enemyspawner;
    spawnenemys spawnE;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyspawner = GameObject.Find("EnemySpawner");
        spawnE = enemyspawner.GetComponent<spawnenemys>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        spawnE.SpawnEnemies();
    }
}