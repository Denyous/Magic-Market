using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button buyButton;

    [Header("Item Data")]
    private ItemDataSO itemData;

    public static event Action OnItemPurchased;
    public void Setup(ItemDataSO data)
    {
        if (data == null)
        {
            Debug.LogError("[ShopItemUI] ItemDataSO null olamaz!");
            return;
        }

        itemData = data;
        // UI elementlerini doldur
        UpdateUI();

        // Buy button listener'ı ekle
        if (buyButton != null)
        {
            buyButton.onClick.AddListener(Buy);
        }
        else
        {
            Debug.LogWarning("[ShopItemUI] buyButton referansı ayarlanmamış!");
        }
    }


    void UpdateUI()
    {
        // Icon ayarla
        if (iconImage != null && itemData.Icon != null)
        {
            iconImage.sprite = itemData.Icon;
        }
        else if (iconImage != null)
        {
            Debug.LogWarning($"[ShopItemUI] {itemData.ItemName} için icon ayarlanmamış!");
        }

        // Item adını ayarla
        if (itemNameText != null)
        {
            itemNameText.text = itemData.ItemName;
        }
        else
        {
            Debug.LogWarning("[ShopItemUI] itemNameText referansı ayarlanmamış!");
        }

        // Fiyatı ayarla
        if (priceText != null)
        {
            priceText.text = itemData.SellValue.ToString("N0");
        }
        else
        {
            Debug.LogWarning("[ShopItemUI] priceText referansı ayarlanmamış!");
        }
    }

    void Buy()
    {
        

        if (MoneyManager.Instance.CanAfford(itemData.SellValue))
        {
            Debug.Log("Ürünü başarıyla satın aldın");
            MoneyManager.Instance.RemoveCoin(itemData.SellValue);
            InventoryManager.Instance.AddItem(itemData);
            OnItemPurchased?.Invoke();

        }
        else
        {
            Debug.LogWarning("Yeterli paran yok!");
        }

    }
}
