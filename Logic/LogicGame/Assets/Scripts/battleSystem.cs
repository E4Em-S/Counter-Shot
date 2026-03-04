using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { Start, Playerturn, EnemyTurn, Won, Lost, RhythmMinigame }
public class battleSystem : MonoBehaviour

{
    public Battlehud playerhud;
    public Battlehud enemyhud;
    public AudioClip damagesound;
    public AudioClip healsound;
    public TextMeshProUGUI battlepaneltext;
    public GameObject playerprefab;
    public GameObject enemyprefab;
    public BattleState state;
    public Transform playerbattlestation;
    public Transform enemybattlestation;
    unit playerunit;
    unit enemyunit;
    Animator enemyanim;
    Animator playeranim;
    public RhythmMinigame rhythmMinigame;
    // Tracks what action triggered the minigame
    private enum PendingAction { Attack, Heal }
    private PendingAction pendingAction;
    // Start is called before the first frame update
    void Awake()
    {

        state = BattleState.Start;
        StartCoroutine(SetupBattle());

        //   StartCoroutine(battlesetup());

    }
    void Start()
    {
        enemyanim =enemyunit.GetComponent<Animator>();
        playeranim = playerunit.GetComponent<Animator>();
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerprefab, playerbattlestation);
        playerunit = playerGO.GetComponent<unit>();
        GameObject enemyGO = Instantiate(enemyprefab, enemybattlestation);
        enemyunit = enemyGO.GetComponent<unit>();
        playerhud.SetHud(playerunit);
        enemyhud.SetHud(enemyunit);
        rhythmMinigame.OnMinigameComplete += HandleMinigameResult;
        yield return new WaitForSeconds(1f);
        state = BattleState.Playerturn;
        Playerturn();
    }
    private void OnDestroy()
    {
        if (rhythmMinigame != null)
            rhythmMinigame.OnMinigameComplete -= HandleMinigameResult;
    }
    void Playerturn()
    {
        battlepaneltext.text = "Player Turn";
    }
    public void Onattackbutton()
    {
        if (state != BattleState.Playerturn) return;

        pendingAction = PendingAction.Attack;
        state = BattleState.RhythmMinigame;
        battlepaneltext.text = "Hit the circles!";

        // 5 circles for attack
        rhythmMinigame.StartMinigame(circleCount: 5, duration: 3.5f, isAttack: true);
    }

    public void Onhealbutton()
    {
        if (state != BattleState.Playerturn) return;

        pendingAction = PendingAction.Heal;
        state = BattleState.RhythmMinigame;
        battlepaneltext.text = "Hit the circles!";

        // 4 circles for heal
        rhythmMinigame.StartMinigame(circleCount: 4, duration: 3.5f, isAttack: false);
    }

   
    void HandleMinigameResult(float accuracyRatio)
    {
        if (pendingAction == PendingAction.Attack)
            StartCoroutine(Playerattack(accuracyRatio));
        else
            StartCoroutine(Playerheal(accuracyRatio));
    }

   
    IEnumerator Playerattack(float accuracy)
    {
        
        float multiplier = Mathf.Lerp(0.6f, 2.5f, accuracy);
        int finalDamage = Mathf.RoundToInt(playerunit.damage * multiplier);

        // Show feedback
        string grade = GetGradeLetter(accuracy);
        battlepaneltext.text = $"{grade}! You deal {finalDamage} damage!";
        playeranim.SetTrigger("attack");
        bool isDead = enemyunit.Takedamage(finalDamage);
        AudioSource.PlayClipAtPoint(damagesound, Camera.main.transform.position);
        enemyhud.sethp(enemyunit.currentHP);

        yield return new WaitForSeconds(2f);

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
    }


    IEnumerator Playerheal(float accuracy)
    {
        
        int baseHeal = Mathf.Max(1, Mathf.RoundToInt(playerunit.maxHP * 0.25f));
        float multiplier = Mathf.Lerp(0.25f, 1.0f, accuracy);
        int finalHeal = Mathf.Max(1, Mathf.RoundToInt(baseHeal * multiplier));

        string grade = GetGradeLetter(accuracy);
        battlepaneltext.text = $"{grade}! You heal {finalHeal} HP!";
        playeranim.SetTrigger("heal");
        playerunit.Heal(finalHeal);
        AudioSource.PlayClipAtPoint(healsound, Camera.main.transform.position);
        playerhud.sethp(playerunit.currentHP);

        yield return new WaitForSeconds(2f);

        state = BattleState.EnemyTurn;
        StartCoroutine(EnemyTurn());
    }

 
    IEnumerator EnemyTurn()
    {
        battlepaneltext.text = "Enemy Turn";
        yield return new WaitForSeconds(1f);
        enemyanim.SetTrigger("attack");
        bool isDead = playerunit.Takedamage(enemyunit.damage);
        AudioSource.PlayClipAtPoint(damagesound, Camera.main.transform.position);
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

    void Endbattle()
    {
        if (state == BattleState.Won)
            battlepaneltext.text = "You Win!";
        else if (state == BattleState.Lost)
            battlepaneltext.text = "You Lose!";
    }

    string GetGradeLetter(float accuracy)
    {
        if (accuracy >= 0.95f) return "S RANK";
        if (accuracy >= 0.80f) return "A RANK";
        if (accuracy >= 0.60f) return "B RANK";
        if (accuracy >= 0.40f) return "C RANK";
        return "D RANK";
    }
}
