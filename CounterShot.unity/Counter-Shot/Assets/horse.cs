using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class horse : MonoBehaviour
{
    [SerializeField] float chargespeed;
    [SerializeField] float rotationspeed = 5f;
    [SerializeField] float chargetime;
    public GameObject player;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        float targetangle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float angle = Mathf.LerpAngle(transform.eulerAngles.z, targetangle, rotationspeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        rb.rotation = angle;
        
    }
    void charge()
    {
        
    }
}
