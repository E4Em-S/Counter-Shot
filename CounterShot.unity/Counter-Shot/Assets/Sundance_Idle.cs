using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sundance_Idle : StateMachineBehaviour
    
{
    int whichattack;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(whichattack);
        whichattack = Random.Range(0, 1);
        Debug.Log(whichattack);
        if(whichattack == 0)
        {
            animator.SetTrigger("Attack1");
        }
        if (whichattack == 1)
        {
            animator.SetTrigger("Attack2");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
   override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack1");
        animator.ResetTrigger("Attack2");
    }
    //IEnumerator chuddelay()
    //{
     //   yield return new WaitForSeconds(2.5f);
    //}


}
