using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinninggun : MonoBehaviour
{
    [SerializeField] float rotationspeed;
    public GameObject Bullet;

    Transform Spawnpoint;

    // Start is called before the first frame update
    void Start()
    {
        Spawnpoint = transform.Find("Bulletspawn");
      
        firebullet();
        
    }



    public void firebullet()
    {
        Instantiate(Bullet, Spawnpoint.transform.position, transform.rotation);
        StartCoroutine(waittforbullet());

    }
     void Update()
    {
        transform.Rotate(0, 0, rotationspeed * Time.deltaTime);
    }

    IEnumerator waittforbullet()
{
    yield return new WaitForSeconds(.5f);
        firebullet();
}
}



