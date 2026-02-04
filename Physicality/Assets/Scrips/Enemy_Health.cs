using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
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
