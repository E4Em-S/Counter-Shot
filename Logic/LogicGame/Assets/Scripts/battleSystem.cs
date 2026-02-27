using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, Playerturn, EnemyTurn, Won, Lost }
public class battleSystem : MonoBehaviour

{
    public Battlehud playerhud;
    public Battlehud enemyhud;
    public TextMeshProUGUI battlepaneltext;
    public GameObject playerprefab;
    public GameObject enemyprefab;
    public BattleState state;
    public Transform playerbattlestation;
    public Transform enemybattlestation;
    unit playerunit;
    unit enemyunit;
    // Start is called before the first frame update
    void Awake()
    {

        state = BattleState.Start;
        StartCoroutine(SetupBattle());

        //   StartCoroutine(battlesetup());

    }
    void Start()
    {

    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerprefab, playerbattlestation);
        playerunit = playerGO.GetComponent<unit>();
        GameObject enemyGO = Instantiate(enemyprefab, enemybattlestation);
        enemyunit = enemyGO.GetComponent<unit>();
        playerhud.SetHud(playerunit);
        enemyhud.SetHud(enemyunit);
        yield return new WaitForSeconds(1f);
        state = BattleState.Playerturn;
        Playerturn();
    }
    void Playerturn()
    {
        battlepaneltext.text = "Player Turn";
    }
    IEnumerator Playerheal()
    {
        playerunit.Heal(1);
        playerhud.sethp(playerunit.currentHP);
        yield return new WaitForSeconds(1f);
        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());    
    }
    public void Onattackbutton()
    {
        if (state != BattleState.Playerturn)
        {
            return;

        }
        StartCoroutine(Playerattack());
    }
    public void Onhealbutton()
    {
        if (state != BattleState.Playerturn)
        {
            return;

        }
        StartCoroutine(Playerheal());
    }
    IEnumerator Playerattack()
    {
        bool isDead = enemyunit.Takedamage(playerunit.damage);
        enemyhud.sethp(enemyunit.currentHP);
        if (isDead)
        {
            state = BattleState.Won;
            Endbattle();
        }
        else
        {
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
        }

        yield return new WaitForSeconds(2f);
    }
    void Endbattle()
    {
        if (state == BattleState.Won)
        {
            battlepaneltext.text = "You Win!";
        }
        else if(state == BattleState.Lost)
        {
            battlepaneltext.text = "You Lose!";
        }
    }
    IEnumerator EnemyTurn()
    {
        battlepaneltext.text = "Enemy Turn";
        yield return new WaitForSeconds(1f);
        bool isDead = playerunit.Takedamage(enemyunit.damage);
     
        playerhud.sethp(playerunit.currentHP);
        yield return new WaitForSeconds(1f);
        if (isDead)
        {
        state = BattleState.Lost;
            Endbattle();
        }
        else
        {
            state = BattleState.Playerturn;
            Playerturn();
        }
    }
}
