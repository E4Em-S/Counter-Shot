using UnityEngine;
using TMPro;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] float maxhp;
    float currenthp;
    bool isdead;
    [SerializeField] TextMeshProUGUI healthtext;
    [SerializeField] RectTransform healthtextrect;
    Camera maincamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currenthp = maxhp;
        maincamera = Camera.main;
        updateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthtextrect != null && healthtext != null)
        {
            Vector3 screenPos = maincamera.WorldToScreenPoint(transform.position + Vector3.up * 0.5f);
            healthtextrect.position = screenPos;
        }
        if (currenthp <= 0 && !isdead)
        {
            Die();
        }
    }
    public void TakeDamage(float amount)
    {
        currenthp -= amount;
        updateUI();
        Debug.Log(currenthp);
    }
    void Die()
    {
        isdead = true;
        Destroy(gameObject);
        Debug.Log(" enemy is dead");
    }
    void updateUI()
    {
        if(healthtext != null)
        {
            healthtext.text = currenthp + "/" + maxhp;
        }
    }
}
