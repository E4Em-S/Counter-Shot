using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmMinigame : MonoBehaviour
{
    [Header("References")]
    public GameObject circlePrefab;    
    public RectTransform playArea;       
    public CanvasGroup canvasGroup;      

    [Header("Tuning")]
    [Tooltip("How long each circle's ring takes to shrink (seconds)")]
    public float circleShrinkTime = 0.8f;

    [Tooltip("Padding from edges (percentage 0-1)")]
    public float edgePadding = 0.15f;

    // Event fired when all circles are resolved. Passes accuracy 0-1.
    public event Action<float> OnMinigameComplete;

    private List<float> scores = new List<float>();
    private int totalCircles;
    private int resolvedCircles;
    private bool isActive;

    
    public void StartMinigame(int circleCount, float duration, bool isAttack)
    {
        gameObject.SetActive(true);
        if (canvasGroup != null) canvasGroup.alpha = 1f;

        scores.Clear();
        usedPositions.Clear();
        totalCircles = circleCount;
        resolvedCircles = 0;
        isActive = true;

        StartCoroutine(SpawnCircles(circleCount, duration, isAttack));
    }

    IEnumerator SpawnCircles(int count, float duration, bool isAttack)
    {
        float interval = duration / count;

        for (int i = 0; i < count; i++)
        {
            SpawnOneCircle(isAttack);
            yield return new WaitForSeconds(interval);
        }
    }

    [Tooltip("Minimum distance between circles (percentage of play area)")]
    public float minCircleDistance = 0.25f;

    private List<Vector2> usedPositions = new List<Vector2>();

    void SpawnOneCircle(bool isAttack)
    {
        float xMin = edgePadding;
        float xMax = 1f - edgePadding;
        float yMin = edgePadding;
        float yMax = 1f - edgePadding;

        // Try to find a position that doesn't overlap existing circles
        Vector2 normPos = Vector2.zero;
        bool foundGoodSpot = false;

        for (int attempt = 0; attempt < 20; attempt++)
        {
            float normX = UnityEngine.Random.Range(xMin, xMax);
            float normY = UnityEngine.Random.Range(yMin, yMax);
            normPos = new Vector2(normX, normY);

            bool tooClose = false;
            foreach (Vector2 used in usedPositions)
            {
                if (Vector2.Distance(normPos, used) < minCircleDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                foundGoodSpot = true;
                break;
            }
        }

        // If no good spot found after 20 tries just use the last random position
        usedPositions.Add(normPos);

        Vector2 areaSize = playArea.rect.size;
        Vector2 localPos = new Vector2(
            (normPos.x - 0.5f) * areaSize.x,
            (normPos.y - 0.5f) * areaSize.y
        );

        GameObject circleGO = Instantiate(circlePrefab, playArea);
        RectTransform rt = circleGO.GetComponent<RectTransform>();
        rt.anchoredPosition = localPos;

        RhythmCircle circle = circleGO.GetComponent<RhythmCircle>();
        circle.Initialize(circleShrinkTime, isAttack, OnCircleResolved);
    }

    
    void OnCircleResolved(float score)
    {
        scores.Add(score);
        resolvedCircles++;

        if (resolvedCircles >= totalCircles)
        {
            StartCoroutine(FinishMinigame());
        }
    }

    IEnumerator FinishMinigame()
    {
        isActive = false;

        // Brief pause so player can see the last verdict
        yield return new WaitForSeconds(0.5f);

        // Calculate average accuracy
        float total = 0f;
        foreach (float s in scores) total += s;
        float accuracy = scores.Count > 0 ? total / scores.Count : 0f;

        // Fade out and hide
        if (canvasGroup != null)
        {
            float t = 0f;
            while (t < 0.3f)
            {
                t += Time.deltaTime;
                canvasGroup.alpha = 1f - (t / 0.3f);
                yield return null;
            }
        }

        gameObject.SetActive(false);

        // Notify the battle system
        OnMinigameComplete?.Invoke(accuracy);
    }
}
