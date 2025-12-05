using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMe : MonoBehaviour
{
    public float killAfter;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, killAfter);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
