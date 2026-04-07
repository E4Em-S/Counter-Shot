using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class SlotMachineUI : MonoBehaviour
{
    [Header("=== Text Displays ===")]
    public TextMeshProUGUI creditsText;
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI payoutText;

    [Header("=== Buttons ===")]
    public Button spinButton;

    [Header("=== Bet Selector ===")]
    [Tooltip("Text showing the current bet amount")]
    public TextMeshProUGUI betText;

    [Tooltip("Button to increase bet")]
    public Button increaseBetButton;

    [Tooltip("Button to decrease bet")]
    public Button decreaseBetButton;

    [Tooltip("Minimum bet allowed")]
    public int minimumBet = 1000;

    [Tooltip("How much each button press changes the bet")]
    public int betStep = 1000;

    [Header("=== Animation Settings ===")]
    [Tooltip("Duration of the credit count-up animation")]
    public float creditAnimDuration = 0.5f;

    [Tooltip("Color flash on jackpot")]
    public Color jackpotColor = new Color(1f, 0.84f, 0f); // Gold
    public Color winColor = new Color(0.4f, 1f, 0.4f);    // Green
    public Color loseColor = new Color(0.7f, 0.7f, 0.7f);  // Gray
    public Color defaultColor = Color.white;

    private int displayedCredits;
    private int currentBet;
    private int playerCredits;
    private Coroutine creditAnimCoroutine;
    public GameObject WinPanel;
    public GameObject LosePanel;

    // Callback to notify SlotMachine when bet changes
    public System.Action<int> OnBetChanged;

    public void Initialize(int startingCredits, int startingBet, System.Action onSpinClicked)
    {
        playerCredits = startingCredits;
        displayedCredits = startingCredits;
        currentBet = Mathf.Max(startingBet, minimumBet);

        UpdateCreditsImmediate(startingCredits);
        UpdateBetDisplay();
        SetMessage("Push the button!", defaultColor);
        ClearPayout();

        if (spinButton != null)
            spinButton.onClick.AddListener(() => onSpinClicked?.Invoke());

        if (increaseBetButton != null)
            increaseBetButton.onClick.AddListener(IncreaseBet);

        if (decreaseBetButton != null)
            decreaseBetButton.onClick.AddListener(DecreaseBet);

        UpdateBetButtons();
    }

   
    /// Increase bet by one step, capped at player's current credits.
    public void IncreaseBet()
    {
        int newBet = currentBet + betStep;
        if (newBet <= playerCredits)
        {
            currentBet = newBet;
            UpdateBetDisplay();
            UpdateBetButtons();
            OnBetChanged?.Invoke(currentBet);
        }
    }
    private void Start()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }


    /// Decrease bet by one step, won't go below minimum.
    public void DecreaseBet()
    {
        int newBet = currentBet - betStep;
        if (newBet >= minimumBet)
        {
            currentBet = newBet;
            UpdateBetDisplay();
            UpdateBetButtons();
            OnBetChanged?.Invoke(currentBet);
        }
    }


    /// Get the current bet amount.
    public int GetCurrentBet()
    {
        return currentBet;
    }

    /// Update credits display with a counting animation.
    public void UpdateCredits(int newCredits)
    {
        playerCredits = newCredits;

        if (creditAnimCoroutine != null)
            StopCoroutine(creditAnimCoroutine);

        creditAnimCoroutine = StartCoroutine(AnimateCredits(displayedCredits, newCredits));
        displayedCredits = newCredits;

        UpdateBetButtons();
    }

  
    /// Set credits immediately without animation.
    public void UpdateCreditsImmediate(int credits)
    {
        playerCredits = credits;
        displayedCredits = credits;
        if (creditsText != null)
            creditsText.text = credits.ToString();

        UpdateBetButtons();
    }

    public void SetMessage(string msg, Color color)
    {
        if (messageText != null)
        {
            messageText.text = msg;
            messageText.color = color;
        }
    }

    public void SetMessage(string msg)
    {
        SetMessage(msg, defaultColor);
    }

    public void ShowSpinning()
    {
        SetMessage("Spinning...", defaultColor);
        ClearPayout();
        SetBetButtonsInteractable(false);
    }

    public void ShowWin(int payout)
    {
       
       
        SetMessage($"Nice! +{payout} credits", winColor);
        ShowPayout(payout);
        SetBetButtonsInteractable(true);
    }

    public void ShowJackpot(int payout)
    {
        Camerashake.shake(duration: 1f, strength: 0.7f);
        SetMessage($"JACKPOT! +{payout} credits!", jackpotColor);
        ShowPayout(payout);
        SetBetButtonsInteractable(true);
    }

    public void ShowLose()
    {
        Camerashake.shake(duration: 0.2f, strength: 0.6f);
        SetMessage("Try again!", loseColor);
        ClearPayout();
        SetBetButtonsInteractable(true);
    }

    public void ShowNotEnoughCredits()
    {
        SetMessage("Not enough credits!", loseColor);
    }

    public void ShowPlayerWin()
    {
       
       
        WinPanel.SetActive(true);
        Debug.Log("win");
}
    public void ShowPlayerLose()
    {
        LosePanel.SetActive(true);
    }
    public void SetSpinButtonInteractable(bool interactable)
    {
        if (spinButton != null)
            spinButton.interactable = interactable;
    }

    
    /// Disable/enable bet buttons during spin.
    public void SetBetButtonsInteractable(bool interactable)
    {
        if (interactable)
        {
            UpdateBetButtons();
        }
        else
        {
            if (increaseBetButton != null) increaseBetButton.interactable = false;
            if (decreaseBetButton != null) decreaseBetButton.interactable = false;
        }
    }

    private void UpdateBetDisplay()
    {
        if (betText != null)
            betText.text = currentBet.ToString();
    }
    public void SetBetAmount(int newBet)
    {
        currentBet = newBet;
        UpdateBetDisplay();
        UpdateBetButtons();
    }

    private void UpdateBetButtons()
    {
        if (decreaseBetButton != null)
            decreaseBetButton.interactable = (currentBet - betStep) >= minimumBet;

        if (increaseBetButton != null)
            increaseBetButton.interactable = (currentBet + betStep) <= playerCredits;
    }

    private void ShowPayout(int amount)
    {
        if (payoutText != null)
        {
            payoutText.text = $"+{amount}";
            payoutText.color = jackpotColor;
        }
    }

    private void ClearPayout()
    {
        if (payoutText != null)
            payoutText.text = "";
    }

    private IEnumerator AnimateCredits(int from, int to)
    {
        float elapsed = 0f;

        while (elapsed < creditAnimDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / creditAnimDuration;
            t = 1f - (1f - t) * (1f - t);
            int current = Mathf.RoundToInt(Mathf.Lerp(from, to, t));

            if (creditsText != null)
                creditsText.text = current.ToString();

            yield return null;
        }

        if (creditsText != null)
            creditsText.text = to.ToString();
    }
}
