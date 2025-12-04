using System.Collections;
using System.Collections.Generic;
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


    

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0f;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        cc = animator.GetComponent<CapsuleCollider2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        rb = animator.GetComponent<Rigidbody2D>(); // GET RIGIDBODY
        pricklyPete = GameObject.Find("PricklyPete");
        animator.applyRootMotion = false;


        if (spriteRenderer != null)
        {
            //spriteRenderer.enabled = false;
        }

        Transform followCircleTransform = animator.transform.Find("followcircle");

        if (followCircleTransform != null)
        {
            followcircle = followCircleTransform.gameObject;
            followcircle.SetActive(true);
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
        timer += Time.deltaTime;
        if (player == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(animator.transform.position, player.position);
       // animator.applyRootMotion = false;
       /* animator.transform.position = Vector3.MoveTowards(
            animator.transform.position,
            player.position,
           followSpeed * Time.deltaTime);
        Debug.Log("Animator position" + spriteRenderer.transform.position);
       */
        //Debug.Log(distanceToPlayer);
        // Move towards player

        //Vector3 direction = (player.position - animator.transform.position).normalized;

        //Vector3 nextPos = animator.transform.position + direction * followSpeed * Time.deltaTime;
        //animator.transform.position = nextPos;
        //Debug.Log(nextPos);

        //animator.transform.position = player.position;

        
        followcircle.transform.position = Vector3.MoveTowards(
            followcircle.transform.position,
            player.position,
           followSpeed * Time.deltaTime);
        //Debug.Log("circle position" + followcircle.transform.position);
        

        //Debug.Log(animator.transform.position);
        
        // TRIGGER TRANSITION AFTER DURATION
        if (timer >= followDuration)
        {
            Debug.Log("Timer complete, triggering descend @ " + followcircle.transform.position);
            animator.transform.position = followcircle.transform.position;
            animator.SetTrigger("decend");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("=== STATE EXIT CALLED ===");
        animator.applyRootMotion = true;
        animator.transform.position = animator.rootPosition;

        // Re-enable physics
        if (rb != null)
        {
            rb.gravityScale = originalGravity;
            Debug.Log("Rigidbody gravity restored");
        }

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        if (cc != null)
        {
            cc.enabled = true;
        }

        if (followcircle != null)
        {
           // followcircle.SetActive(false);
        }
    }
}
