using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class throwgun : StateMachineBehaviour
{
   
    public GameObject spingun;
     Transform gunspawn;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        gunspawn = animator.transform.Find("Gunthowspawn");
        
        Instantiate(spingun, gunspawn.position, gunspawn.rotation);
          
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

   
}
