using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("Variebles")]
    
    [SerializeField] private List<ItemDataSO> items = new List<ItemDataSO>();
    [SerializeField] private ShopItemUI shopItemPrefab;
    [SerializeField] private Transform contentParent;


    void Start()
    {
        InitializeShop();
    }


    public void InitializeShop()
    {
        // Null check yaparak güvenli bir şekilde devam et
        if (shopItemPrefab == null)
        {
            Debug.LogError("[ShopUI] shopItemPrefab referansı ayarlanmamış!");
            return;
        }

        if (contentParent == null)
        {
            Debug.LogError("[ShopUI] contentParent referansı ayarlanmamış!");
            return;
        }

        if (items == null || items.Count == 0)
        {
            Debug.LogWarning("[ShopUI] Market'te item yok!");
            return;
        }

        foreach (ItemDataSO itemData in items)
        {
            if (itemData == null)
            {
                Debug.LogWarning("[ShopUI] items listesinde null bir item var, atlanacak.");
                continue;
            }
            ShopItemUI shopItemUI = Instantiate(shopItemPrefab, contentParent);
            shopItemUI.Setup(itemData);
        }
    }


}
