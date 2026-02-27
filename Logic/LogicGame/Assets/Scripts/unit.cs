using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit : MonoBehaviour
{
    public string unitname;
    public int damage;
    public int maxHP;
    public int currentHP;

    public bool Takedamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}
