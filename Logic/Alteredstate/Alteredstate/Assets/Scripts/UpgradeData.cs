using UnityEngine;


/// Defines a single tier of an upgrade.

[System.Serializable]
public class UpgradeTier
{
    [Tooltip("Cost to purchase this tier")]
    public int cost;

    [Tooltip("The value this tier provides (meaning depends on upgrade type)")]
    public float value;

    [Tooltip("Description shown to the player")]
    public string description;
}


/// Types of upgrades available.

public enum UpgradeType
{
    BetterOdds,
    HigherPayouts,
    ExtraReels,
    LowerGoal,
    LoseProtection
}

/// Defines a single upgrade with multiple tiers.

[System.Serializable]
public class Upgrade
{
    public string name;
    public UpgradeType type;
    public UpgradeTier[] tiers;

    [HideInInspector]
    public int currentTier = -1; // -1 means not purchased

    
    /// Returns true if there are more tiers to buy.
  
    public bool CanUpgrade()
    {
        return currentTier < tiers.Length - 1;
    }

    
    /// Gets the next tier's cost, or -1 if maxed out.
  
    public int GetNextCost()
    {
        if (!CanUpgrade()) return -1;
        return tiers[currentTier + 1].cost;
    }

  
    /// Gets the next tier's description.

    public string GetNextDescription()
    {
        if (!CanUpgrade()) return "MAX LEVEL";
        return tiers[currentTier + 1].description;
    }

    
    /// Gets the current tier's value, or 0 if not purchased.
    
    public float GetCurrentValue()
    {
        if (currentTier < 0) return 0f;
        return tiers[currentTier].value;
    }


    /// Gets the current level (0 = not purchased, 1 = tier 0, etc.)
  
    public int GetLevel()
    {
        return currentTier + 1;
    }

    /// Gets the max level.
    public int GetMaxLevel()
    {
        return tiers.Length;
    }


    /// Purchase the next tier. Returns true if successful.
    public bool Purchase()
    {
        if (!CanUpgrade()) return false;
        currentTier++;
        return true;
    }
}


[CreateAssetMenu(fileName = "UpgradeData", menuName = "SlotMachine/UpgradeData")]
public class UpgradeData : ScriptableObject
{
    public Upgrade[] upgrades;

    
    /// Find an upgrade by type.
    public Upgrade GetUpgrade(UpgradeType type)
    {
        foreach (var upgrade in upgrades)
        {
            if (upgrade.type == type)
                return upgrade;
        }
        return null;
    }

    
    public void ResetAll()
    {
        foreach (var upgrade in upgrades)
            upgrade.currentTier = -1;
    }
}

