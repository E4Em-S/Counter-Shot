using System.Collections;
using UnityEngine;

/// Main slot machine controller. Coordinates reels, payout logic, and UI.
/// Now supports upgrades via UpgradeManager.
public class SlotMachine : MonoBehaviour
{
    [Header("=== Core References ===")]
    public PayTable payTable;
    public Reel[] reels = new Reel[3];
    public SlotMachineUI ui;
    public UpgradeManager upgradeManager;

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

    [HideInInspector]
    public int reelCount = 3;

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
        reelCount = reels.Length;

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
            ui.UpdateGoalText(creditsToWin);
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

        // Determine final results for all active reels
        int[] finalResults = new int[reelCount];

        // Check for Better Odds upgrade
        float oddsBonus = 0f;
        if (upgradeManager != null)
            oddsBonus = upgradeManager.GetUpgradeValue(UpgradeType.BetterOdds);

        for (int i = 0; i < reelCount; i++)
        {
            finalResults[i] = payTable.GetRandomSymbolIndex();

            // Better Odds: chance to match previous reel
            if (i > 0 && oddsBonus > 0f)
            {
                if (Random.value < oddsBonus)
                    finalResults[i] = finalResults[i - 1];
            }
        }

        // Calculate reel durations (each reel spins a bit longer)
        float[] reelDurations = new float[reelCount];
        for (int i = 0; i < reelCount; i++)
            reelDurations[i] = spinDuration * (0.5f + (0.5f / reelCount) * i);

        // Start all reels spinning
        for (int i = 0; i < reelCount; i++)
        {
            if (i < reels.Length && reels[i] != null)
                StartCoroutine(reels[i].SpinAndStop(reelDurations[i], finalResults[i]));
        }

        // Wait for all reels to finish
        yield return new WaitForSeconds(spinDuration + 0.2f);

        // Calculate payout
        int payout = CalculatePayout(finalResults);

        if (payout > 0)
        {
            credits += payout;

            if (IsAllMatching(finalResults))
            {
                ui?.ShowJackpot(payout);
                PlaySound(jackpotSound);
            }
            else
            {
                ui?.ShowWin(payout);
                PlaySound(winSound);
            }
        }
        else
        {
            // Lose Protection: refund part of the bet
            float loseProtection = 0f;
            if (upgradeManager != null)
                loseProtection = upgradeManager.GetUpgradeValue(UpgradeType.LoseProtection);

            if (loseProtection > 0f)
            {
                int refund = Mathf.RoundToInt(betAmount * loseProtection);
                credits += refund;
                ui?.ShowLose();
                // Message could show the refund but ShowLose keeps it simple
            }
            else
            {
                ui?.ShowLose();
            }

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

        if (credits < betAmount && credits >= ui.minimumBet)
        {
            int newBet = (credits / ui.betStep) * ui.betStep;
            if (newBet < ui.minimumBet) newBet = ui.minimumBet;
            betAmount = newBet;
            ui?.SetBetAmount(newBet);
        }

        // Check lose condition
        if (credits < ui.minimumBet)
        {
            ui?.ShowPlayerLose();
            ui?.SetSpinButtonInteractable(false);
            ui?.SetBetButtonsInteractable(false);
            OnPlayerLose?.Invoke();
            OnSpinResult?.Invoke(finalResults, payout, credits);
            isSpinning = false;
            yield break;
        }

        ui?.SetSpinButtonInteractable(credits >= betAmount);

        // Fire event
        OnSpinResult?.Invoke(finalResults, payout, credits);

        isSpinning = false;
    }

    /// Calculate payout with support for 3, 4, or 5 reels and upgrade multipliers.
    private int CalculatePayout(int[] results)
    {
        int basePayout = 0;

        if (reelCount == 3)
        {
            basePayout = payTable.CalculatePayout(results, betAmount);
        }
        else if (reelCount >= 4)
        {
            int maxMatch = GetMaxMatchingCount(results);
            int symbolIndex = GetMostCommonSymbol(results);

            if (maxMatch >= reelCount)
            {
                // All reels match — mega jackpot
                basePayout = payTable.symbols[symbolIndex].payoutMultiplier * betAmount * 3;
            }
            else if (maxMatch >= reelCount - 1)
            {
                // All but one match
                basePayout = payTable.symbols[symbolIndex].payoutMultiplier * betAmount * 2;
            }
            else if (maxMatch >= 3)
            {
                // 3 match
                basePayout = payTable.symbols[symbolIndex].payoutMultiplier * betAmount;
            }
            else if (maxMatch >= 2)
            {
                // 2 match
                basePayout = Mathf.FloorToInt(betAmount * payTable.twoMatchMultiplier);
            }
        }

        // Apply Higher Payouts upgrade multiplier
        if (basePayout > 0 && upgradeManager != null)
        {
            float payoutMultiplier = upgradeManager.GetUpgradeValue(UpgradeType.HigherPayouts);
            if (payoutMultiplier > 0f)
                basePayout = Mathf.RoundToInt(basePayout * payoutMultiplier);
        }

        return basePayout;
    }

    /// Check if all results match.
    private bool IsAllMatching(int[] results)
    {
        for (int i = 1; i < results.Length; i++)
        {
            if (results[i] != results[0]) return false;
        }
        return true;
    }

    /// Get the highest count of any single symbol in results.
    private int GetMaxMatchingCount(int[] results)
    {
        int maxCount = 0;
        for (int i = 0; i < results.Length; i++)
        {
            int count = 0;
            for (int j = 0; j < results.Length; j++)
            {
                if (results[j] == results[i]) count++;
            }
            if (count > maxCount) maxCount = count;
        }
        return maxCount;
    }

    /// Get the symbol index that appears most often.
    private int GetMostCommonSymbol(int[] results)
    {
        int bestSymbol = results[0];
        int bestCount = 0;

        for (int i = 0; i < results.Length; i++)
        {
            int count = 0;
            for (int j = 0; j < results.Length; j++)
            {
                if (results[j] == results[i]) count++;
            }
            if (count > bestCount)
            {
                bestCount = count;
                bestSymbol = results[i];
            }
        }
        return bestSymbol;
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
