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
    [SerializeField] public GameObject enemyHealthBar;
    public bool isVisible;
    int ogLayoutLength;
    public void UpdateHealthBar(int dam)
    {
        enemyHealthBar.gameObject.SetActive(true);

        ogLayoutLength = layout.transform.childCount; //store original length of layoutGroup
        for (int i = ogLayoutLength - 1; i >= ogLayoutLength - dam; i--) //set length of og group; subtract by the amnt of damage
        {
            if (i < 0) break; // Prevent out-of-range
            Animator chunkHealth = layout.transform.GetChild(i).GetComponent<Animator>();
            chunkHealth.SetTrigger("damage");
            //Destroy(i);
        }
        StartCoroutine(waitforsec());
        //Debug.Log("SetHealthBar inactive");
    }
    public void Destroy(int i)
    {
        Destroy(layout.transform.GetChild(i).gameObject);//destroys 
    }
    public void HideHealthBar()
    {
        enemyHealthBar.gameObject.SetActive(false);
    }
    IEnumerator waitforsec()
    {
        yield return new WaitForSecondsRealtime(1f);
        enemyHealthBar.gameObject.SetActive(false);

    }
   
}
