using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinninggun : MonoBehaviour
{
    [SerializeField] float rotationspeed;
    [SerializeField] Sprite parryableBulletSprite;
    public GameObject Bullet;
    int parrychance;

    Transform Spawnpoint;
    [SerializeField] AudioSource gunShotSound;

    // Start is called before the first frame update
    void Start()
    {
        Spawnpoint = transform.Find("Bulletspawn");
      
        firebullet();
        
    }



    public void firebullet()
    {
        gunShotSound.Play();
        parrychance = Random.Range(0, 5);
        GameObject spawnedbullet = Instantiate(Bullet, Spawnpoint.position, Spawnpoint.rotation);
        if (parrychance == 3 || parrychance == 4)
        { 
             SpriteRenderer sprite = spawnedbullet.GetComponent<SpriteRenderer>();
            sprite.color = new UnityEngine.Color(1f, 0f, 0.82f);
            //FIGURE OUT HOW TO SET SPRITE TO PINK ONE HERE
            //spawnedbullet.GetComponent<SpriteRenderer>().
            spawnedbullet.tag = "Parryable";
            spawnedbullet.AddComponent<Parryablebullet>(); //adds parryable bullet script to the obj
        }
       
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



