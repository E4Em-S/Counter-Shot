using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class horse : MonoBehaviour
{
    [SerializeField] float chargespeed;
    [SerializeField] float rotationspeed = 5f;
    [SerializeField] float chargeCooldown;
    public Vector2 target;
    public Rigidbody2D rb;
    bool lookAt;

    public GameObject playerposition;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform.position;
  
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(ChargeCooldown());
        /*   Vector2 direction = (player.transform.position - transform.position).normalized;
           float targetangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetangle, rotationspeed * Time.deltaTime);
           transform.rotation = Quaternion.Euler(0f, 0f, angle);

           rb.rotation = angle;
           */
    }
    void charge()
    {
        // Vector3 direction = player.transform.position - transform.position; //get player location 
        //direction.Normalize(); //& normalize
        transform.Translate(target * Time.deltaTime * chargespeed, Space.World); //move enemy
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, target);
        toRotation *= Quaternion.Euler(0f, 0f, 180f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed);
    }
    IEnumerator ChargeCooldown()
    {
        Debug.Log("Wind up animation for horse here");
        charge();
        resetHorse();
        yield return new WaitForSeconds(chargeCooldown);

    }
    void resetHorse()
    {
        target = GameObject.FindWithTag("Player").transform.position;
    }

    // THIS IS THE MAGIC SAUCE: smoothly move currentQuantity to desiredQuantity:
    void ProcessMovement()
    {
        // Every frame without exception move the currentQuantity
        // towards the desiredQuantity, by the movement rate:
        //transform.position = Mathf.MoveTowards(transform.position, target.transform.position, chargespeed * Time.deltaTime);

    }
}
