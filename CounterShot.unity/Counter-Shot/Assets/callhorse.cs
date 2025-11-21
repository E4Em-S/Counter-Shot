using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class callhorse : StateMachineBehaviour
{
    public GameObject horse;
    Transform horsepoint;
    BoxCollider2D box;
   
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        horsepoint = animator.transform.Find("HorseSpawn");
        box = animator.GetComponent<BoxCollider2D>();
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Instantiate(horse, horsepoint.position, horsepoint.rotation);
        box.enabled = true;
         
    }

   
}
