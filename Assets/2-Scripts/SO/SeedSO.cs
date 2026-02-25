using UnityEngine;


[CreateAssetMenu(fileName = "SeedSO", menuName = "Scriptable Objects/SeedSO")]
/// <summary>
/// Tarım sistemi için tohum item türü.
/// Büyüme süresi, hasat ürünü ve miktarını tanımlar.
/// </summary>
public class SeedSO : ItemDataSO
{
    
    [SerializeField]
    [Tooltip("Crop sistemi için Crop ID tanımlayıcısı")]
    private string cropId;

    [SerializeField]
    [Range(10f, 3600f)]
    [Tooltip("Büyüme süresi (saniye)")]
    private float growthDuration = 60f;

    [SerializeField]
    [Tooltip("Hasat sonucu elde edilecek item")]
    private ItemDataSO yieldItem;

    [SerializeField]
    [Range(1, 100)]
    [Tooltip("Hasat sonucu miktar")]
    private int yieldAmount = 1;

    #region Properties

    public string CropId => cropId;
    public float GrowthDuration => growthDuration;
    public ItemDataSO YieldItem => yieldItem;
    public int YieldAmount => yieldAmount;

    #endregion

    public override bool Validate()
    {
        if (!base.Validate()) return false;

        if (string.IsNullOrWhiteSpace(cropId))
        {
            Debug.LogError($"[SeedItemSO] {name}: CropId boş olamaz!", this);
            return false;
        }

        if (growthDuration <= 0)
        {
            Debug.LogError($"[SeedItemSO] {name}: GrowthDuration 0'dan büyük olmalı!", this);
            return false;
        }

        if (yieldItem == null)
        {
            Debug.LogError($"[SeedItemSO] {name}: YieldItem atanmalı!", this);
            return false;
        }

        // Recursive seed reference check
        if (yieldItem is SeedSO)
        {
            Debug.LogWarning($"[SeedItemSO] {name}: YieldItem başka bir seed - bu niyetli midir?", this);
        }

        if (yieldAmount <= 0)
        {
            Debug.LogError($"[SeedItemSO] {name}: YieldAmount 0'dan büyük olmalı!", this);
            return false;
        }

        return true;
    }
}

   

