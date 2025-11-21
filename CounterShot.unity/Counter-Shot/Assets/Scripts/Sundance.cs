using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sundance : MonoBehaviour
{
    public Transform bulletspawn;
    int parrychance;
    [SerializeField] AudioSource gunShotSound;

    public GameObject bullet;
    bool horseSpawned = false;
    public EnemyHealth EH;
    Animator anim;
    public BoxCollider2D box;
    
    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EH.currentHealth <= 30 && horseSpawned == false)
        {
             box.enabled = false;
            anim.SetTrigger("CallHorse");
            horseSpawned = true;
          

        }
    }
    public void FireGun()
    {
        parrychance = Random.Range(0, 5);
        GameObject spawnedbullet = Instantiate(bullet, bulletspawn.position, bulletspawn.rotation);
        gunShotSound.Play();
        if (parrychance == 3 || parrychance == 4)
        { 
             SpriteRenderer sprite = spawnedbullet.GetComponent<SpriteRenderer>();
            sprite.color = new UnityEngine.Color(1f, 0f, 0.82f);
            spawnedbullet.tag = "Parryable";
            spawnedbullet.AddComponent<Parryablebullet>();
            
        }
       
    }
   
}
