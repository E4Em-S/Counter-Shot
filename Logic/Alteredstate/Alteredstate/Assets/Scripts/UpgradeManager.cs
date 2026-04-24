using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Header("=== References ===")]
    public UpgradeData upgradeData;
    public SlotMachine slotMachine;
    public UpgradeUI upgradeUI;

    [Header("=== Extra Reels (for 5-reel upgrade) ===")]
    [Tooltip("Pre-placed but disabled reel GameObjects for the 5-reel upgrade")]
    public Reel extraReel1;
    public Reel extraReel2;

    [Header("=== Audio ===")]
    public AudioClip upgradeSound;
    private AudioSource audioSource;

    /// Event fired when any upgrade is purchased.

    public System.Action<UpgradeType, int> OnUpgradePurchased;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        // Reset upgrades at game start
        upgradeData.ResetAll();

        // Hide extra reels at start
        if (extraReel1 != null) extraReel1.gameObject.SetActive(false);
        if (extraReel2 != null) extraReel2.gameObject.SetActive(false);

        // Initialize UI
        if (upgradeUI != null)
            upgradeUI.Initialize(upgradeData, TryPurchaseUpgrade);
    }

    /// Attempt to purchase an upgrade. Returns true if successful.
   
    public bool TryPurchaseUpgrade(UpgradeType type)
    {
        Upgrade upgrade = upgradeData.GetUpgrade(type);
        if (upgrade == null) return false;
        if (!upgrade.CanUpgrade()) return false;

        int cost = upgrade.GetNextCost();
        if (slotMachine.GetCredits() < cost) return false;

        // Deduct credits
        slotMachine.AddCredits(-cost);
        // Check if player can no longer afford minimum bet
        if (slotMachine.GetCredits() < slotMachine.ui.minimumBet)
        {
            slotMachine.ui?.ShowPlayerLose();
            slotMachine.ui?.SetSpinButtonInteractable(false);
            slotMachine.ui?.SetBetButtonsInteractable(false);
            slotMachine.OnPlayerLose?.Invoke();
            upgradeUI?.ClosePanel();
            return true;
        }

        // Purchase the tier
        upgrade.Purchase();
        if (upgradeSound != null && audioSource != null)
            audioSource.PlayOneShot(upgradeSound);

        // Apply the effect
        ApplyUpgrade(type);

        // Update UI
        upgradeUI?.RefreshAll();

        // Fire event
        OnUpgradePurchased?.Invoke(type, upgrade.GetLevel());

        return true;
    }

  
    /// Apply the effect of an upgrade to the slot machine.
   
    private void ApplyUpgrade(UpgradeType type)
    {
        Upgrade upgrade = upgradeData.GetUpgrade(type);
        if (upgrade == null) return;

        switch (type)
        {
            case UpgradeType.LowerGoal:
                // Value represents the new goal amount
                slotMachine.creditsToWin = Mathf.RoundToInt(upgrade.GetCurrentValue());
                slotMachine.ui?.UpdateGoalText(slotMachine.creditsToWin);
                break;

            case UpgradeType.ExtraReels:
                // Level 1 = 4 reels, Level 2 = 5 reels
                ActivateExtraReel(upgrade.GetLevel());
                break;

                // BetterOdds, HigherPayouts, and LoseProtection are checked
                // during spin in SlotMachine via GetUpgradeValue()
        }
    }

    private void ActivateExtraReel(int level)
    {
        if (level == 1 && extraReel1 != null)
        {
            // Activate 4th reel
            extraReel1.gameObject.SetActive(true);
            extraReel1.Initialize(slotMachine.payTable);

            Reel[] newReels = new Reel[4];
            for (int i = 0; i < slotMachine.reels.Length; i++)
                newReels[i] = slotMachine.reels[i];
            newReels[3] = extraReel1;
            slotMachine.reels = newReels;
            slotMachine.reelCount = 4;
        }
        else if (level == 2 && extraReel2 != null)
        {
            // Activate 5th reel
            extraReel2.gameObject.SetActive(true);
            extraReel2.Initialize(slotMachine.payTable);

            Reel[] newReels = new Reel[5];
            for (int i = 0; i < slotMachine.reels.Length; i++)
                newReels[i] = slotMachine.reels[i];
            newReels[4] = extraReel2;
            slotMachine.reels = newReels;
            slotMachine.reelCount = 5;
        }
    }


    /// Get the current value of an upgrade. Used by SlotMachine during spins.
   
    public float GetUpgradeValue(UpgradeType type)
    {
        Upgrade upgrade = upgradeData.GetUpgrade(type);
        if (upgrade == null) return 0f;
        return upgrade.GetCurrentValue();
    }

    
    /// Check if an upgrade has been purchased at least once.
    
    public bool HasUpgrade(UpgradeType type)
    {
        Upgrade upgrade = upgradeData.GetUpgrade(type);
        if (upgrade == null) return false;
        return upgrade.GetLevel() > 0;
    }
}

