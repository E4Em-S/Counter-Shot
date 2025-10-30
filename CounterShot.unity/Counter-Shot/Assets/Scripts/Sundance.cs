using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sundance : MonoBehaviour
{
    public Transform bulletspawn;
    int parrychance;

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
        parrychance = Random.Range(0, 5);
        GameObject spawnedbullet = Instantiate(bullet, bulletspawn.position, bulletspawn.rotation);
        if(parrychance == 3 || parrychance == 4)
        { 
             SpriteRenderer sprite = spawnedbullet.GetComponent<SpriteRenderer>();
            sprite.color = new UnityEngine.Color(1f, 0.75f, 0.8f);
            spawnedbullet.tag = "Parryable";
            spawnedbullet.AddComponent<Parryablebullet>();
        }
       
    }
   
}
