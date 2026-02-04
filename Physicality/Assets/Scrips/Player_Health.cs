using UnityEngine;

public class Player_Health : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject[] lives;
    [SerializeField] float maxhp;
    float currenthp;
    bool isdead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (currenthp <= 0 && !isdead)
        {
            Die();
        }
        if (currenthp < 1)
        {
            Destroy(lives[0].gameObject);
        }
        else if (currenthp < 2)
        {
            Destroy(lives[1].gameObject);
        }
        else if (currenthp < 3)
        {
            Destroy(lives[2].gameObject);
        }
    }
    public void TakeDamage(float amount)
    {
        currenthp -= amount;
        Debug.Log(currenthp);
    }
    void Die()
    {
        isdead = true;
        Debug.Log("Player is dead");
    }
}
