using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Sundance : MonoBehaviour
{
    public Transform bulletspawn;
    int parrychance;
    [SerializeField] AudioSource gunShotSound;
    [SerializeField] AudioSource Laughsound;
    [SerializeField] AudioSource callhorsesound;
    [SerializeField] Sprite parryBulletImg;

    public GameObject bullet;
    bool horseSpawned = false;
    public EnemyHealth EH;
    Animator anim;
    public BoxCollider2D box;
    
    // Start is called before the first frame update
    void Start()
    {
        Laughsound.Play();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(EH.currentHealth <= 40 && horseSpawned == false)
        {
             box.enabled = false;
            anim.SetTrigger("CallHorse");
            
            horseSpawned = true;
          

        }
    }
    public void FireGun()
    {
        parrychance = Random.Range(0, 5);
        Sprite originalSprite = bullet.GetComponent<SpriteRenderer>().sprite;
        
        GameObject spawnedbullet = Instantiate(bullet, bulletspawn.position, bulletspawn.rotation);

        gunShotSound.Play();
        if (parrychance == 3 || parrychance == 4)
        {
            
            SpriteRenderer sprite = spawnedbullet.GetComponent<SpriteRenderer>();
            
            

            spawnedbullet.tag = "Parryable";

            Parryablebullet parryScript = spawnedbullet.AddComponent<Parryablebullet>();
            parryScript.parrybulletsprite = parryBulletImg;
           
            sprite.color = new UnityEngine.Color(1f, 0f, 0.82f);

            if (parryBulletImg == null)
                Debug.LogError("parryBulletImg is not assigned!");

        }
       
    }
    public void playsound()
    {
        callhorsesound.Play();
    }
   
}
