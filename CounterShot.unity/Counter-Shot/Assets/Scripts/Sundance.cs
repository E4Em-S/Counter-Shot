using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sundance : MonoBehaviour
{
    public Transform bulletspawn;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FireGun()
    {
        Instantiate(bullet, bulletspawn.position, bulletspawn.rotation);
    }
}
