using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class petefollowplayer : StateMachineBehaviour
{
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float verticalOffset = 0f;
    [SerializeField] private float followDuration = 10f;

    private Transform player;
    private CapsuleCollider2D cc;
    private SpriteRenderer spriteRenderer;
    private GameObject followcircle;
    private Rigidbody2D rb; 
    private float timer = 0f;
    private float originalGravity;
    private GameObject pricklyPete;
    private bool isJumpAttacking;
    private Transform followCircleDetached;


    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;
        isJumpAttacking = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cc = animator.GetComponent<CapsuleCollider2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        rb = animator.GetComponent<Rigidbody2D>(); // GET RIGIDBODY
        pricklyPete = GameObject.Find("PricklyPete");

        followCircleDetached = GameObject.FindGameObjectWithTag("followCircleDetached").transform;


        animator.applyRootMotion = false;
        if(rb != null)
        {
            originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
        }
        
        if (spriteRenderer != null)
        {
            //spriteRenderer.enabled = false;
        }

        Transform followCircleTransform = animator.transform.Find("followcircle");

        if (followCircleTransform != null)
        {
            followcircle = followCircleTransform.gameObject;
            followcircle.SetActive(true);

            //unparent
            followcircle.transform.SetParent(null);
            //set to pete position
            followcircle.transform.position = animator.transform.position;
        }
        else
        {
            Debug.LogError("followcircle not found as child of " + animator.name);
        }

        if (cc != null)
        {
            cc.enabled = false;
        }

        Debug.Log("Follow state entered");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        followCircleDetached = GameObject.FindGameObjectWithTag("followCircleDetached").transform;
        timer += Time.deltaTime;
        if (player == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(animator.transform.position, player.position);
        

        if (isJumpAttacking)
        {
            //Vector2 targetPosition = Vector2.MoveTowards(animator.transform.position, player.position, followSpeed * Time.deltaTime);
            //followcircle.transform.position = Vector3.MoveTowards(followcircle.transform.position, player.position, followSpeed * Time.deltaTime);
              if(rb != null)
        {
            //rb.MovePosition(targetPosition);
        }

        else
        {
            //animator.transform.position = followcircle.transform.position;
        }

        }
        
        
        //Debug.Log("circle position" + followcircle.transform.position);
        

        //Debug.Log(animator.transform.position);
        
        // TRIGGER TRANSITION AFTER DURATION
        if (timer >= followDuration)
        {
            //Debug.Log("Timer complete, triggering descend @ " + followcircle.transform.position);
            //animator.transform.position = followcircle.transform.position;
            animator.SetTrigger("decend");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("EXIT Jump attack exiting");

        //Vector3 beforePosition = animator.transform.position;
        

          //Vector3 targetPosition = followCircleDetached.transform.position;

        animator.applyRootMotion = false;
        isJumpAttacking = false;

        //clear physics
        if(rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.gravityScale = originalGravity;
        }
         if(followcircle != null)
        {
            followcircle.transform.SetParent(animator.transform);
            followcircle.transform.localPosition = Vector3.zero;
        }
         if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        //animator.transform.position = followCircleDetached.transform.position;
        //rb.transform.position = followCircleDetached.transform.position;
          
        animator.applyRootMotion = false;
        
        Debug.Log("=== STATE EXIT CALLED ===");
  // Re-enable physics
       // if (rb != null && followcircle != null)
      //  {
      //      rb.velocity = Vector2.zero;
            //rb.position = followcircle.transform.position;
           
     //   }
       
                
       /* if (cc != null)
        {
            cc.enabled = true;
        }
        */
        animator.applyRootMotion = false;
       Debug.Log($"END of OnStateExit - Animator at: {animator.transform.position}");
    }
}
