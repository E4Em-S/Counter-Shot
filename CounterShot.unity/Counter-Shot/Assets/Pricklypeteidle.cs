using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pricklypeteidle : StateMachineBehaviour
{
    int whichattack;
    int attack1 = 0;
    int attack2 = 0;
    int attack3 = 0;
    private int lastAttack = -1;
    private int consecutiveAttackCount = 0;
    int maxattackvalue;
    bool callPetersGaruntee;

    bool firstturnedhalfhp = false;
    CapsuleCollider2D bc;
    chuddywaitforsec chud;
    EnemyHealth eh;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bc= animator.GetComponent<CapsuleCollider2D>();
        chud = animator.GetComponent<chuddywaitforsec>();
        eh = animator.GetComponent<EnemyHealth>();
        if (eh.currentHealth <= 40 && firstturnedhalfhp == false)
        {
            animator.SetTrigger("Half");
            bc.enabled = false;
           

            
            firstturnedhalfhp = true;
            whichattack = 4;
            
            Debug.Log(whichattack);
           
        }
        else
        {
            chud.StartCoroutine(chud.waiting(() => TriggerAttack(animator)));
        }
        
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Spikeshot");
        animator.ResetTrigger("Jump");
        animator.ResetTrigger("Call");
    }
    void TriggerAttack(Animator animator)
    {
       if (whichattack == 4)
        {
            
            animator.SetTrigger("Call");
            callPetersGaruntee = true;
            Debug.Log("call peters");
            bc.enabled = true;
        }
        

        if (eh.currentHealth <= 40 && firstturnedhalfhp == true)
        {
            maxattackvalue = 1;
        }
        else
        {
            maxattackvalue = 1;
        }
        if(callPetersGaruntee == true)
        {
            whichattack = 2;
            callPetersGaruntee = false;
        }
        else {
            whichattack = Random.Range(0, maxattackvalue);
        }
       
        if(whichattack == lastAttack && consecutiveAttackCount >=1)
        {
            int attempts = 0;
            while (whichattack == lastAttack && attempts < 10) // Failsafe
            {
                whichattack = Random.Range(0, maxattackvalue);
                attempts++;
            }
            consecutiveAttackCount = 1;
        }
        else if (whichattack == lastAttack)
        {
            consecutiveAttackCount++;
        }
        else
        {
            consecutiveAttackCount = 1;
        }
        lastAttack = whichattack;

        // Execute the selected attack
        switch (whichattack)
        {
            case 0:
                Debug.Log("spikes firing from here");
                animator.SetTrigger("Spikeshot");
                break;

            case 1:
                Debug.Log("Whichattack is 2 PETER");
                animator.SetTrigger("Call");
                break;

            case 2:
                //animator.SetTrigger("Jump");
                break;
                
        }








        /* if (attack1 == 2)
         {
             whichattack = 1;
             attack1 = 0;
         }
         if (attack2 == 2)
         {
             whichattack = 0;
             attack2 = 0;
         }
         if( attack3 ==2)
         {

             attack3 = 0;
         }
         if (whichattack == 0)
         {
             Debug.Log("spikes firing from here");
             animator.SetTrigger("Spikeshot");
             attack1++;
         }

         else if (whichattack == 1)
         {
             animator.SetTrigger("Jump");
             attack2++;
         }
         else if (whichattack == 2) {
             Debug.Log("Whitchattack is 2");
             animator.SetTrigger("Call");
             attack3++;
         }*/
    }
    }

  