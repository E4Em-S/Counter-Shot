using UnityEngine;


/// Defines a single slot machine symbol with its sprite and payout multiplier.
[System.Serializable]
public class SlotSymbol
{
    public string name;
    public Sprite sprite;
    [Tooltip("Payout multiplier when 3 of this symbol land")]
    public int payoutMultiplier;
}



[CreateAssetMenu(fileName = "PayTable", menuName = "SlotMachine/PayTable")]
public class PayTable : ScriptableObject
{
    [Header("=== Symbols ===")]
    public SlotSymbol[] symbols;

    [Header("=== Partial Match ===")]
    [Tooltip("Multiplier applied to bet when 2 symbols match (e.g., 1.5 = 150% of bet)")]
    public float twoMatchMultiplier = 1.5f;

  
    /// Returns a random symbol index.
    public int GetRandomSymbolIndex()
    {
        return Random.Range(0, symbols.Length);
    }

    /// <summary>
    /// Returns the sprite for a given symbol index.
    /// </summary>
    public Sprite GetSprite(int index)
    {
        if (index < 0 || index >= symbols.Length) return null;
        return symbols[index].sprite;
    }

   
    /// Calculates the payout for a set of 3 reel results.
    public int CalculatePayout(int[] results, int betAmount)
    {
        int a = results[0], b = results[1], c = results[2];

        // Three of a kind
        if (a == b && b == c)
            return symbols[a].payoutMultiplier * betAmount;

        // Two of a kind
        if (a == b || b == c || a == c)
            return Mathf.FloorToInt(betAmount * twoMatchMultiplier);

        return 0;
    }

   
    /// Returns true if all three results match.
    public bool IsJackpot(int[] results)
    {
        return results[0] == results[1] && results[1] == results[2];
    }
}