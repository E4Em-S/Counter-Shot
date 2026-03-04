
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class RhythmCircle : MonoBehaviour, IPointerClickHandler
{
    [Header("References (assign in prefab)")]
    public RectTransform outerRing;
    public Image innerCircleImage;
    public Image outerRingImage;
    public TextMeshProUGUI verdictText;

    [Header("Colors")]
    public Color attackColor = new Color(1f, 0.23f, 0.36f, 1f);   
    public Color healColor = new Color(0.23f, 1f, 0.69f, 1f);     
    public Color perfectColor = new Color(1f, 0.84f, 0f, 1f);     
    public Color greatColor = new Color(0.49f, 0.99f, 0f, 1f);    
    public Color goodColor = new Color(0f, 0.75f, 1f, 1f);        
    public Color missColor = new Color(1f, 0.27f, 0.27f, 1f);     

    [Header("Timing Windows (seconds from perfect center)")]
    public float perfectWindow = 0.045f;
    public float greatWindow = 0.09f;
    public float goodWindow = 0.14f;

    [Header("Sizes")]
    public float innerSize = 64f;
    public float outerStartSize = 140f; 

    // Internals
    private float shrinkDuration;
    private float elapsed;
    private bool resolved;
    private bool isAttack;
   public AudioClip clicksound;
    private Action<float> onResolved;

    
    public void Initialize(float shrinkTime, bool attack, Action<float> callback)
    {
        shrinkDuration = shrinkTime;
        isAttack = attack;
        onResolved = callback;
        elapsed = 0f;
        resolved = false;

        // Set colors
        Color accent = attack ? attackColor : healColor;
        innerCircleImage.color = accent;
        outerRingImage.color = new Color(accent.r, accent.g, accent.b, 0.7f);

        
        if (verdictText != null)
        {
            verdictText.gameObject.SetActive(false);
        }
           

       
        GetComponent<RectTransform>().sizeDelta = new Vector2(innerSize, innerSize);
        outerRing.sizeDelta = new Vector2(outerStartSize, outerStartSize);
    }

    void Update()
    {
        if (resolved) return;

        elapsed += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsed / shrinkDuration);

        // Shrink outer ring from outerStartSize down to innerSize
        float currentSize = Mathf.Lerp(outerStartSize, innerSize, progress);
        outerRing.sizeDelta = new Vector2(currentSize, currentSize);

        // Pulse the outer ring opacity
        float pulse = 0.5f + 0.3f * Mathf.Sin(elapsed * 8f);
        Color c = outerRingImage.color;
        outerRingImage.color = new Color(c.r, c.g, c.b, pulse);

        // If ring fully shrunk and player didn't click → miss
        if (progress >= 1f)
        {
            Resolve(0f, "MISS", missColor);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (resolved) return;

        // How far off from the perfect moment (when elapsed == shrinkDuration)?
        float delta = Mathf.Abs(elapsed - shrinkDuration);

        if (delta <= perfectWindow)
            Resolve(1.0f, "Amazing", perfectColor);
        else if (delta <= greatWindow)
            Resolve(0.7f, "Good", greatColor);
        else if (delta <= goodWindow)
            Resolve(0.4f, "OK", goodColor);
        else
            Resolve(0f, "Miss", missColor);
    }

    void Resolve(float score, string label, Color labelColor)
    {
        resolved = true;

        outerRing.gameObject.SetActive(false);

      
        innerCircleImage.color = labelColor;
        AudioSource.PlayClipAtPoint(clicksound, Camera.main.transform.position);

        SpawnVerdictText(label, labelColor);

       
        onResolved?.Invoke(score);

     
        Destroy(gameObject, 0.8f);
    }

    void SpawnVerdictText(string label, Color color)
    {
        
        GameObject textGO = new GameObject("Verdict");
        textGO.transform.SetParent(transform.parent, false); 

        RectTransform rt = textGO.AddComponent<RectTransform>();
        // Position it at the same spot as the circle
        rt.anchoredPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 80f);
        rt.sizeDelta = new Vector2(200, 50);

        TextMeshProUGUI tmp = textGO.AddComponent<TextMeshProUGUI>();
        tmp.text = label;
        tmp.color = Color.white;
        tmp.fontSize = 28;
        tmp.fontStyle = FontStyles.Bold;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.raycastTarget = false;

        // Destroy the floating text after a moment
        Destroy(textGO, 0.7f);
    }
}
