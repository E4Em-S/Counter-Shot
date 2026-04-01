using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reel : MonoBehaviour
{
    [Tooltip("Image component displaying the current symbol")]
    public Image symbolImage;

    [Header("=== Animation Settings ===")]
    [Tooltip("How fast symbols cycle during spin (seconds between changes)")]
    public float tickInterval = 0.08f;

    [Tooltip("Optional: punch scale effect when reel stops")]
    public float stopPunchScale = 1.15f;
    public float stopPunchDuration = 0.15f;

    private PayTable payTable;
    private int currentSymbolIndex;
    private bool isStopped = false;

    /// <summary>
    /// Initialize this reel with a reference to the pay table.
    /// </summary>
    public void Initialize(PayTable table)
    {
        payTable = table;

        if (symbolImage == null)
            symbolImage = GetComponent<Image>();

        // Show a random symbol on start
        SetSymbol(payTable.GetRandomSymbolIndex());
    }

    /// <summary>
    /// Spins the reel for the given duration, then lands on finalSymbolIndex.
    /// </summary>
    public IEnumerator SpinAndStop(float duration, int finalSymbolIndex)
    {
        isStopped = false;
        float elapsed = 0f;
        float nextTick = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (elapsed >= nextTick)
            {
                // Show random symbols while spinning
                int randIndex = payTable.GetRandomSymbolIndex();
                SetSymbol(randIndex);
                nextTick = elapsed + tickInterval;
            }

            yield return null;
        }

        // Land on final symbol
        SetSymbol(finalSymbolIndex);
        isStopped = true;

        // Punch scale effect
        yield return PunchScale();
    }

    /// <summary>
    /// Immediately set the displayed symbol.
    /// </summary>
    public void SetSymbol(int symbolIndex)
    {
        currentSymbolIndex = symbolIndex;
        Sprite sprite = payTable.GetSprite(symbolIndex);
        if (symbolImage != null && sprite != null)
            symbolImage.sprite = sprite;
    }

    public int GetCurrentSymbolIndex()
    {
        return currentSymbolIndex;
    }

    public bool IsStopped()
    {
        return isStopped;
    }

    private IEnumerator PunchScale()
    {
        Vector3 original = transform.localScale;
        Vector3 punched = original * stopPunchScale;
        float half = stopPunchDuration * 0.5f;

        // Scale up
        float t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(original, punched, t / half);
            yield return null;
        }

        // Scale back down
        t = 0f;
        while (t < half)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(punched, original, t / half);
            yield return null;
        }

        transform.localScale = original;
    }
}

