using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;

public class Sundance_Idle : StateMachineBehaviour

{

    int whichattack;
    int attack1 = 0;
    int attack2 = 0;
    private BoxCollider2D sundanceHitbox;
     chuddywaitforsec chud;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        sundanceHitbox = animator.GetComponent<BoxCollider2D>();
         chud = animator.GetComponent<chuddywaitforsec>();
        //Debug.Log(whichattack);
        sundanceHitbox.enabled = true;
        chud.StartCoroutine(chud.waiting(() => TriggerAttack(animator)));
      
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
    void TriggerAttack(Animator animator)
    {
      
        whichattack = Random.Range(0, 2);
        //Debug.Log(whichattack);
        if (attack1 == 2)
        {
            whichattack = 1;
            attack1 = 0;
        }
        if (attack2 == 2)
        {
            whichattack = 0;
            attack2 = 0;
        }
        if(whichattack == 0)
        {
            animator.SetTrigger("Attack1");
            attack1++;
        }
        else if (whichattack == 1)
        {
            animator.SetTrigger("Attack2");
            attack2++;
        }
    }


}
