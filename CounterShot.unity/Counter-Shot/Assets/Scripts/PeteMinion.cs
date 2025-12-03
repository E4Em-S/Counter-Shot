using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeteMinion : MonoBehaviour
{
    public GameObject bullet;
    Transform bulletpos;
    float timer;
    int parrychance;
    // Start is called before the first frame update
    void Start()
    {
        bulletpos = transform.Find("spikespawn");
        StartCoroutine(waittodie());
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
        GameObject spike = Instantiate(bullet, bulletpos.position, Quaternion.identity);
        if (parrychance == 3 || parrychance == 4)
        {
            SpriteRenderer sprite = spike.GetComponent<SpriteRenderer>();
            sprite.color = new UnityEngine.Color(1f, 0f, 0.82f);
            spike.tag = "Parryable";
            spike.AddComponent<Parryablebullet>();

        }
    }
    IEnumerator waittodie()
    {
        yield return new WaitForSeconds(7);
        Destroy(gameObject);
    }
}
