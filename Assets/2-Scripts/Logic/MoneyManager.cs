using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public TextMeshProUGUI _moneyText;
    public float Money { get; private set; } = 300f;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateMoneyText();
        }
        {
            Destroy(gameObject);
        }

    }

    public void AddCoin(float amount)
    {
        Money += amount;
        UpdateMoneyText();
    }

    public void RemoveCoin(float amount)
    {
        if (CanAfford(amount))
        {
            Money -= amount;
            UpdateMoneyText();
        }
        else
        {
           Debug.LogWarning($"Cannot remove {amount} coins. Not enough money.");
        }
    }

    public bool CanAfford(float amount)
    {
        return Money >= amount;
    }

    private void UpdateMoneyText()
    {
        _moneyText.text = $"Coin: {Money}";
    }

    public float GetMoney()
    {
        return Money;
    }

}
