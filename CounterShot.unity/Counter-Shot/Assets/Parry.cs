using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Parry : MonoBehaviour
{
    [Header("parrying")]
    public bool isParrying = false;
    [SerializeField] int pps; //parries per sec
    [SerializeField] Collider2D parryZone;
    int ammo = 6;
    // Start is called before the first frame update
    void Start()
    {
        //parryZone.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnParry(InputValue parryValue)
    {
        Debug.Log("parrying");
        isParrying = true;
        parryZone.enabled = true;
        StartCoroutine(parry());
    }
    IEnumerator parry()
    {
        parryZone.enabled = true;
        yield return new WaitForSeconds(pps);
        parryZone.enabled = false;
        isParrying = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isParrying == true)
        {
            if (collision.tag == "Parryable")
            {
                ammo++;
                Debug.Log("parry collision w: " + collision.gameObject);
               // Destroy(collision.gameObject);
            }
            if (ammo > 6)
            {
                ammo = 6;
            }
        }
    }
    
}
