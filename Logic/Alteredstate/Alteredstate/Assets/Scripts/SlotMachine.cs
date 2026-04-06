using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class SlotMachine : MonoBehaviour
{
    [Header("=== Core References ===")]
    public PayTable payTable;
    public Reel[] reels = new Reel[3];
    public SlotMachineUI ui;

    [Header("=== Game Settings ===")]
    public int startingCredits = 10000;
    public int defaultBet = 1000;

    [Header("=== Win Condition ===")]
    [Tooltip("Credits needed to win the game")]
    public int creditsToWin = 100000;

    [Header("=== Spin Timing ===")]
    [Tooltip("Total spin duration in seconds")]
    public float spinDuration = 2.0f;

    [Tooltip("Delay between each reel stopping (creates cascading effect)")]
    public float reelStopDelay = 0.4f;

    [Header("=== Audio (Optional) ===")]
    public AudioClip spinSound;
    public AudioClip winSound;
    public AudioClip jackpotSound;
    public AudioClip loseSound;

    private int credits;
    private int betAmount;
    private bool isSpinning = false;
    private AudioSource audioSource;

  
    /// Event fired after each spin completes.
    public System.Action<int[], int, int> OnSpinResult;

 
    /// Event fired when the player wins.
    public System.Action OnPlayerWin;

    //Event fired when the player loses.
    public System.Action OnPlayerLose;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        credits = startingCredits;
        betAmount = defaultBet;

        // Initialize reels
        foreach (var reel in reels)
        {
            if (reel != null)
                reel.Initialize(payTable);
        }

        // Initialize UI with bet selector
        if (ui != null)
        {
            ui.Initialize(startingCredits, defaultBet, Spin);
            ui.OnBetChanged += OnBetChanged;
        }
    }

    private void OnDestroy()
    {
        if (ui != null)
            ui.OnBetChanged -= OnBetChanged;
    }

  
    /// Called when the player changes their bet via the UI
    private void OnBetChanged(int newBet)
    {
        betAmount = newBet;
    }

    /// Trigger a spin. Called by the UI button or from code.
    public void Spin()
    {
        if (isSpinning) return;

        // Get the current bet from UI
        if (ui != null)
            betAmount = ui.GetCurrentBet();

        if (credits < betAmount)
        {
            ui?.ShowNotEnoughCredits();
            return;
        }

        StartCoroutine(SpinRoutine());
    }

    /// Add credits externally (shop, reward, etc.).
    public void AddCredits(int amount)
    {
        credits += amount;
        ui?.UpdateCredits(credits);
    }

    /// Get the current credit balance.
    public int GetCredits() => credits;


    /// Get the current bet amount.
    public int GetBetAmount() => betAmount;


    /// Check if the machine is currently spinning.
    public bool IsSpinning() => isSpinning;

    private IEnumerator SpinRoutine()
    {
        isSpinning = true;

        // Deduct bet
        credits -= betAmount;
        ui?.UpdateCreditsImmediate(credits);
        ui?.ShowSpinning();
        ui?.SetSpinButtonInteractable(false);

        PlaySound(spinSound);

        // Determine final results
        int[] finalResults = new int[3];
        for (int i = 0; i < 3; i++)
            finalResults[i] = payTable.GetRandomSymbolIndex();

        // Calculate reel durations (each reel spins a bit longer)
        float[] reelDurations = new float[3];
        for (int i = 0; i < 3; i++)
            reelDurations[i] = spinDuration * (0.5f + 0.25f * i);

        // Start all reels spinning
        for (int i = 0; i < 3; i++)
        {
            if (reels[i] != null)
                StartCoroutine(reels[i].SpinAndStop(reelDurations[i], finalResults[i]));
        }

        // Wait for all reels to finish
        yield return new WaitForSeconds(spinDuration + 0.2f);

        // Calculate payout
        int payout = payTable.CalculatePayout(finalResults, betAmount);
        credits += payout;

        // Update UI based on result
        if (payout > 0)
        {
            if (payTable.IsJackpot(finalResults))
            {

                ui?.ShowJackpot(payout);
                PlaySound(jackpotSound);
            }
            else
            {
                Camerashake.shake(duration: 2f, strength: 2f);
                ui?.ShowWin(payout);
                PlaySound(winSound);
            }
        }
        else
        {
            ui?.ShowLose();
            PlaySound(loseSound);
        }

        ui?.UpdateCredits(credits);

        // Check win condition
        if (credits >= creditsToWin)
        {
            ui?.ShowPlayerWin();
            ui?.SetSpinButtonInteractable(false);
            ui?.SetBetButtonsInteractable(false);
            OnPlayerWin?.Invoke();
            OnSpinResult?.Invoke(finalResults, payout, credits);
            isSpinning = false;
            yield break;
        }
        if (credits <= 0)
        {
            ui?.ShowPlayerLose();
            ui?.SetSpinButtonInteractable(false);
            ui?.SetBetButtonsInteractable(false);
            OnPlayerLose?.Invoke();
            OnSpinResult?.Invoke(finalResults, payout, credits);
            isSpinning = false;
            yield break;
        }

        if (credits < betAmount && credits >= ui.minimumBet)
        {
            int newBet = (credits / ui.betStep) * ui.betStep;
            if (newBet < ui.minimumBet) newBet = ui.minimumBet;
            betAmount = newBet;
            ui?.SetBetAmount(newBet);
        }

        ui?.SetSpinButtonInteractable(credits >= ui.minimumBet);

        // Fire event
        OnSpinResult?.Invoke(finalResults, payout, credits);

        isSpinning = false;
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }

#if UNITY_EDITOR
    [ContextMenu("Print Pay Table")]
    private void PrintPayTable()
    {
        if (payTable == null || payTable.symbols == null) return;

        Debug.Log("=== SLOT MACHINE PAY TABLE ===");
        foreach (var sym in payTable.symbols)
            Debug.Log($"{sym.name} x3 = {sym.payoutMultiplier}x bet ({sym.payoutMultiplier * betAmount} credits)");
        Debug.Log($"Any 2 matching = {payTable.twoMatchMultiplier}x bet ({Mathf.FloorToInt(betAmount * payTable.twoMatchMultiplier)} credits)");
    }
#endif
}