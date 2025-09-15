using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem;
public class CharacterController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [Header("changeables")]
    [SerializeField] public float moveSpeed;

    Vector2 lookDir;
    [SerializeField] Transform gunBaseTransform;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMove(InputValue value) 
    {
        var moveDir = value.Get<Vector2>();
        rb.velocity = moveDir * moveSpeed;
    }
    public void OnLook(InputValue lookValue)
    {
        lookDir = lookValue.Get<Vector2>().normalized;
        if(Mathf.Abs(lookDir.magnitude)> 0)
        {
            gunBaseTransform.up = lookDir;
        }
    }
}


