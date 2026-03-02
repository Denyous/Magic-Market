using UnityEngine;
using UnityEngine.UI;

public class EnergyManager : MonoBehaviour
{

    [Header("Variables")]
    public static EnergyManager Instance { get; private set; }

    private const int MaxEnergy = 100;
    public int CurrentEnergy { get; private set; }

    [Header("UI Elements")]

    [SerializeField] private Slider _energySlider;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            CurrentEnergy = MaxEnergy;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void OnDecreasePressed()
    {
        DecreaseEnergy(10);
    }
    public void OnIncreasedPressed()
    {
        IncreaseEnergy(10);
    }

    public void IncreaseEnergy(float amount)
    {
        if (CurrentEnergy == MaxEnergy)
        {
            Debug.Log("Energy is already at maximum. Cannot increase further.");
        }
        else
        {
            CurrentEnergy += (int)amount;
            
            if (CurrentEnergy > MaxEnergy)
            {
                CurrentEnergy = MaxEnergy;
            }
            
            UpdateUI();

        }
    }

    public void DecreaseEnergy(float amount)
    {
        if (CurrentEnergy <= 0)
        {
            Debug.Log("Energy is already at 0. Cannot decrease further. You feel tired.");
        }
        else
        {
            CurrentEnergy -= (int)amount;
            UpdateUI();
        }
    }
    public void UpdateUI()
    {
        _energySlider.value = CurrentEnergy;
    }
}
