using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pricklyPeteLanding : StateMachineBehaviour
{
    private Transform followCircleDetached;
    private Vector3 targetPosition;
    private bool isForcing = false;
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followCircleDetached = GameObject.FindGameObjectWithTag("followCircleDetached").transform;
        targetPosition = followCircleDetached.transform.position;

         Debug.Log($"[LANDING] Pete position BEFORE: {animator.transform.position}");
         Debug.Log($"[LANDING] Target (followCircleDetached): {followCircleDetached.transform.position}");
         
         
         Rigidbody2D rb = animator.GetComponent<Rigidbody2D>();
         CapsuleCollider2D cc = animator.GetComponent<CapsuleCollider2D>();

         if(rb != null)
        {
            Debug.Log("LANDING RB position BEFORE" + rb.position);
            rb.velocity = Vector2.zero;
        }
        if(cc != null)
        {
            cc.enabled = false;
        }
        //move pete
        animator.transform.position = targetPosition;
        if(rb != null) //move rb too
        {
            rb.position = targetPosition;
        }


        isForcing = true;

         Debug.Log($"[LANDING] Pete position AFTER: {animator.transform.position}");
      // ⭐ ADD THIS - Track position over next few frames
        MonoBehaviour mb = animator.GetComponent<MonoBehaviour>();
        if(mb != null)
        {
            mb.StartCoroutine(ForcePositionCoroutine(animator, rb, cc));
        }
    }
     // ⭐ force position every frame
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(isForcing)
        {
            animator.transform.position = targetPosition;
            Rigidbody2D rb = animator.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.position = targetPosition;
            }
        }
    }
    
    private System.Collections.IEnumerator ForcePositionCoroutine(Animator animator, Rigidbody2D rb, CapsuleCollider2D cc)
    {
        // Force position for 5 frames
        for(int i = 0; i < 5; i++)
        {
            animator.transform.position = targetPosition;
            if(rb != null)
            {
                rb.position = targetPosition;
            }
            Debug.Log($"[LANDING Forcing Frame {i}] Pete at: {animator.transform.position}");
            yield return null;
        }
        
        isForcing = false;
        // Re-enable physics and collider
        if(rb != null)
        {
            rb.isKinematic = false;
            Debug.Log("[LANDING] Rigidbody re-enabled");
        }
        
}
}


    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

