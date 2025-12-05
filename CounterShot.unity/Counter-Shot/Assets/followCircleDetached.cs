using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCircleDetached : MonoBehaviour
{
    private Transform player;
    [SerializeField] private float followSpeed;
    private Transform pricklyPeteTransform;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pricklyPeteTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetPosition = Vector2.MoveTowards(rb.transform.position, player.position, followSpeed * Time.deltaTime);
        rb.MovePosition(targetPosition);
    }
}
