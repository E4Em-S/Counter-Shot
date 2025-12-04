using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peteland : StateMachineBehaviour
{
    CapsuleCollider2D cc;
    SpriteRenderer spriteRenderer;
    GameObject followcircle;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     cc = animator.GetComponent<CapsuleCollider2D>();
        cc.enabled = true;
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        followcircle = GameObject.Find("followcircle");
        followcircle.SetActive(true);

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      
    }

    
}
