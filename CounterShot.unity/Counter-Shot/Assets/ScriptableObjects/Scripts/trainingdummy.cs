using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trainingdummy : MonoBehaviour
{
    public Transform spawner1;
    public Transform spawner2;
    public Transform spawner3;
    public Transform spawner4;
    public Transform spawner5;
    public Transform spawner6;
    public Transform spawner7;
    public Transform spawner8;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Bullettime());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator Bullettime()
    {
        yield return new WaitForSeconds(3);
        Instantiate(bullet, spawner1.position, spawner1.rotation);
        Instantiate(bullet, spawner2.position, spawner2.rotation);
        Instantiate(bullet, spawner3.position, spawner3.rotation);
        Instantiate(bullet, spawner4.position, spawner4.rotation);
        Instantiate(bullet, spawner5.position, spawner5.rotation);
        Instantiate(bullet, spawner6.position, spawner6.rotation);
        Instantiate(bullet, spawner7.position, spawner7.rotation);
        Instantiate(bullet, spawner8.position, spawner8.rotation);
        StartCoroutine(Bullettime());
    }
}
