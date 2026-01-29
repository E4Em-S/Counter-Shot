using UnityEngine;

public class paddle_movement : MonoBehaviour
{
    public float speed;
    float horizontalmovement;
   [SerializeField] float maxX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalmovement = Input.GetAxis("Horizontal");
        if((horizontalmovement>0 && transform.position.x < maxX)||  (horizontalmovement <0 && transform.position.x > -maxX))
            {

            transform.position += Vector3.right * horizontalmovement * speed * Time.deltaTime;
        }
        

       
    }
}
