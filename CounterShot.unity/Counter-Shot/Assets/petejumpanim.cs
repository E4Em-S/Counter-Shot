using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petejumpanim : StateMachineBehaviour
{
    CapsuleCollider2D cc;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("followplayer");
    }

   
}
