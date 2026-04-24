using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UpgradeUI : MonoBehaviour
{
    [Header("=== Panel ===")]
    [Tooltip("The upgrade panel itself (toggled on/off)")]
    public GameObject upgradePanel;

    [Tooltip("Button to open the upgrade panel")]
    public Button openButton;

    [Tooltip("Button to close the upgrade panel")]
    public Button closeButton;

    [Header("=== Upgrade Rows ===")]
    public UpgradeRow[] upgradeRows;

    private UpgradeData upgradeData;
    private System.Func<UpgradeType, bool> onPurchase;

    public void Initialize(UpgradeData data, System.Func<UpgradeType, bool> purchaseCallback)
    {
        Debug.Log("UpgradeUI Initialize called");
        upgradeData = data;
        onPurchase = purchaseCallback;

        // Hide panel at start
        if (upgradePanel != null)
            upgradePanel.SetActive(false);

        // Set up open/close buttons
        if (openButton != null)
            openButton.onClick.AddListener(OpenPanel);

        if (closeButton != null)
            closeButton.onClick.AddListener(ClosePanel);

        // Initialize each row
        for (int i = 0; i < upgradeRows.Length; i++)
        {
            if (i < data.upgrades.Length)
            {
                int index = i; // capture for closure
                UpgradeRow row = upgradeRows[i];
                Upgrade upgrade = data.upgrades[i];

                if (row.buyButton != null)
                {
                    row.buyButton.onClick.AddListener(() =>
                    {
                        if (onPurchase != null && onPurchase(upgrade.type))
                        {
                            RefreshAll();
                        }
                    });
                }
            }
        }

        RefreshAll();
    }

    public void OpenPanel()
    {
        if (upgradePanel != null)
        {
            upgradePanel.SetActive(true);
            RefreshAll();
        }
    }

    public void ClosePanel()
    {
        if (upgradePanel != null)
            upgradePanel.SetActive(false);
    }

    
    /// Refresh all upgrade rows to show current state.
   
    public void RefreshAll()
    {
        if (upgradeData == null) return;

        for (int i = 0; i < upgradeRows.Length; i++)
        {
            if (i < upgradeData.upgrades.Length)
                RefreshRow(upgradeRows[i], upgradeData.upgrades[i]);
        }
    }

    private void RefreshRow(UpgradeRow row, Upgrade upgrade)
    {
        if (row.nameText != null)
            row.nameText.text = upgrade.name;

        if (row.levelText != null)
            row.levelText.text = $"Lv {upgrade.GetLevel()}/{upgrade.GetMaxLevel()}";

        if (row.descriptionText != null)
            row.descriptionText.text = upgrade.GetNextDescription();

        if (row.costText != null)
        {
            if (upgrade.CanUpgrade())
                row.costText.text = $"{upgrade.GetNextCost()} credits";
            else
                row.costText.text = "MAXED";
        }

        if (row.buyButton != null)
            row.buyButton.interactable = upgrade.CanUpgrade();
    }
}


/// UI elements for a single upgrade row.

[System.Serializable]
public class UpgradeRow
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public Button buyButton;
}
