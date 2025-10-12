using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    float healthValue;
    private Animator chunkHealth;
    [SerializeField] public LayoutGroup layout;
    int ogLayoutLength;
    public void UpdateHealthBar(int dam)
    {
        ogLayoutLength = layout.transform.childCount; //store original length of layoutGroup
        for (int i = ogLayoutLength - 1; i >= ogLayoutLength - dam; i--) //set length of og group; subtract by the amnt of damage
        {
            if (i < 0) break; // Prevent out-of-range
            Animator chunkHealth = layout.transform.GetChild(i).GetComponent<Animator>();
            chunkHealth.SetTrigger("damage");
            //yield return new WaitForSeconds(0.15f);
            //Destroy(i);
        }
    }
    public void Destroy(int i)
    {
        Destroy(layout.transform.GetChild(i).gameObject);//destroys 
    }
   
}
