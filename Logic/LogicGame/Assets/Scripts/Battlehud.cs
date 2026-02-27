using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Battlehud : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public Slider hpslider;


    public void SetHud(unit stats)
    {
        nametext.text = stats.unitname;
        hpslider.maxValue = stats.maxHP;
        hpslider.value = stats.currentHP;
    }
    public void sethp(int hp)
    {
        hpslider.value = hp;
    }
}
