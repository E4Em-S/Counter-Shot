using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinninggun : MonoBehaviour
{
    public float rotationspeed;
    public GameObject Bullet;

    Transform Spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        Spawnpoint = transform.Find("Bulletspawn");
        Debug.Log(Spawnpoint);
    }

    // Update is called once per frame
    void Update()
    {

    }
public void firebullet()
    {
        Instantiate(Bullet, Spawnpoint.position, Spawnpoint.rotation);
    }
   
}
