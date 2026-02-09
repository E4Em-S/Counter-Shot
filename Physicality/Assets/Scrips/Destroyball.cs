using System.Collections;
using UnityEngine;

public class Destroyball : MonoBehaviour
{
    public GameObject ballprefab;
    [SerializeField] Transform ballspawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "ball")
        {
            Destroy(other.gameObject);
            StartCoroutine(spawnnewball());
        }
    }
    IEnumerator spawnnewball()
    {
        yield return new WaitForSeconds(1);
        GameObject ball = Instantiate(ballprefab, ballspawn.position, Quaternion.identity);
    }
}
