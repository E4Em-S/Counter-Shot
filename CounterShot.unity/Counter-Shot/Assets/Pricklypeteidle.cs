using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pricklypeteidle : StateMachineBehaviour
{
    int whichattack;
    int attack1 = 0;
    int attack2 = 0;
    int attack3 = 0;
    chuddywaitforsec chud;
    EnemyHealth eh;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        chud = animator.GetComponent<chuddywaitforsec>();
        eh = animator.GetComponent<EnemyHealth>();
        chud.StartCoroutine(chud.waiting(() => TriggerAttack(animator)));
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Spikeshot");
        animator.ResetTrigger("Jump");
    }
    void TriggerAttack(Animator animator)
    {
        if (eh.currentHealth <= 40)
        {
            whichattack = Random.Range(0, 3);
        }
        else
        {
            whichattack = Random.Range(0, 2);
        }
       
        
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
        if( attack3 ==1)
        {
            whichattack = Random.Range(0, 2);
            attack3 = 0;
        }
        if (whichattack == 0)
        {
            animator.SetTrigger("Spikeshot");
            attack1++;
        }
        else if (whichattack == 1)
        {
            animator.SetTrigger("Jump");
            attack2++;
        }
        else if (whichattack == 2) {
            animator.SetTrigger("Call");
            attack3++;
        }
    }
    }

  