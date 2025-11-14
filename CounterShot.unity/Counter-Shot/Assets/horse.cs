using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class horse : MonoBehaviour
{
    [SerializeField] float chargespeed;
    [SerializeField] float rotationspeed = 5f;
    [SerializeField] float chargeCooldown;
    [SerializeField] float chargeWindup;
    [SerializeField] float overshotDistance = 5f;
    public Vector3 target;
    public Vector3 chargeEndPoint;
    public Rigidbody2D rb;
    bool lookAt;
    SpriteRenderer horseSprite;

    Animator horseanim;
    public GameObject playerposition;
    // Start is called before the first frame update
    void Start()
    {
        horseSprite = GetComponent<SpriteRenderer>();
        horseanim = GetComponent<Animator>();
        StartCoroutine(ChargeLoop());
    }

    // Update is called once per frame
    void Update()
    {

        if (target.x > transform.position.x)
        {
            horseSprite.flipX = true;
        }
        else if (target.x < transform.position.x)
        {
            horseSprite.flipX = false;
        }
    }
    void moveInBounds(Vector3 direction, float speed)
    {
        Vector3 nextPos = transform.position + direction * speed * Time.deltaTime;

        if (nextPos.x < -30f || nextPos.x > 30f)
            nextPos.x = transform.position.x;
        if (nextPos.y < -17f || nextPos.y > 17f)
            nextPos.y = transform.position.y;

        transform.position = nextPos;
    }
    
    void charge(Vector3 targetPoint, float speed)
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetPoint, chargespeed * Time.deltaTime); //move enemy
        Vector3 direction = (targetPoint - transform.position).normalized;


        //rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationspeed * Time.deltaTime);


        Vector3 nextPos = transform.position + direction * speed * Time.deltaTime;

        //bounds check
        if (nextPos.x < -30f || nextPos.x > 30f)
            nextPos.x = transform.position.x;
        if (nextPos.y < -17f || nextPos.y > 17f)
            nextPos.y = transform.position.y;

        transform.position = nextPos;
        
    }
    IEnumerator ChargeLoop()
    {
        horseanim.SetBool("Charge!", true);
        while (true)
        {
            Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position; //save players position
            //windup
            Debug.Log("WindUPAnimhere");
            horseanim.SetTrigger("windup");
            yield return new WaitForSeconds(chargeWindup);

            //overshoot
            Vector3 dir = (playerPos - transform.position).normalized;
            chargeEndPoint = playerPos + dir * overshotDistance;

            chargeEndPoint.x = Mathf.Clamp(chargeEndPoint.x, -30f, 30f);
            chargeEndPoint.y = Mathf.Clamp(chargeEndPoint.y, -17f, 17f);


            while (Vector3.Distance(transform.position, chargeEndPoint) > 0.2f)
            {
                //moveInBounds(chargeEndPoint, chargespeed);

                
                charge(chargeEndPoint, chargespeed);
                yield return null;
            }
            horseanim.SetTrigger("ChargeOver");
            yield return new WaitForSeconds(chargeCooldown);
            

        }

    }
 
}
