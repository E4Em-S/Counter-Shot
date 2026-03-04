using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class Battlehud : MonoBehaviour
{
    public TextMeshProUGUI nametext;
    public TextMeshProUGUI healthtext;
  


    public void SetHud(unit stats)
    {
        nametext.text = stats.unitname;
        healthtext.text = "Health: " + stats.currentHP;
    }
    public void sethp(int hp)
    {
        healthtext.text = "Health: " + hp.ToString();
    }
}
