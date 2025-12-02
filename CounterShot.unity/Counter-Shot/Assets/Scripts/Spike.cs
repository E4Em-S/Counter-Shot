using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(destroyself());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //IEnumerator destroyself()
    // {
    //     yield return new WaitForSeconds(3);
    //    Destroy (gameObject);
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
