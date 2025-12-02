using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeteMinion : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletpos;
    float timer;
    int parrychance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
            timer = 0;
            shoot();
        }  
    }
    void shoot()
    {
        parrychance = Random.Range(0, 5);
        Instantiate(bullet, bulletpos.position, Quaternion.identity);
        if (parrychance == 3 || parrychance == 4)
        {
            SpriteRenderer sprite = bullet.GetComponent<SpriteRenderer>();
            sprite.color = new UnityEngine.Color(1f, 0f, 0.82f);
            bullet.tag = "Parryable";
            bullet.AddComponent<Parryablebullet>();

        }
    }
}
