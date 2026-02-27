using TMPro;
using UnityEngine;

public class PlayerMoneyView : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI moneyText;
    private void OnEnable()
    {
        ShopItemUI.OnItemPurchased += UpdateMoneyText;
    }
    private void OnDisable()
    {
        ShopItemUI.OnItemPurchased -= UpdateMoneyText;
    }

    private void Start()
    {
        UpdateMoneyText();
        
    }
    private void UpdateMoneyText()
    {
        if (moneyText == null)
        {
            return;
        }

       moneyText.text = $"Coin: {MoneyManager.Instance.GetMoney()}";
    }


}
