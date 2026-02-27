using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "Scriptable Objects/ItemDataSO")]
public abstract class ItemDataSO : ScriptableObject
{
    [SerializeField]
    [Tooltip("Benzersiz item tanımlayıcı")]
    private int itemId;

    [SerializeField]
    [Tooltip("Oyuncu görünümü için item adı")]
    private string itemName;

    [SerializeField]
    [TextArea(2, 4)]
    [Tooltip("Item açıklaması")]
    private string description;

    [SerializeField]
    [Tooltip("UI ve world'de görüntülenecek ikon")]
    private Sprite icon;

    [SerializeField]
    [Tooltip("Item türü")]
    private ItemType itemType;

    [SerializeField]
    [Tooltip("Stack edilebilir mi?")]
    private bool stackable;

    [SerializeField]
    [Tooltip("Maksimum stack sayısı")]
    private int maxStack = 1;

    [SerializeField]
    [Tooltip("Satış değeri (ileride ekonomi sistemi için)")]
    private int sellValue = 10;

    /// <summary>
    /// İnspektorda ID validation için callback.
    /// </summary>
    private void OnValidate()
    {
        if (itemId < 0) itemId = 0;
        if (maxStack < 1) maxStack = 1;
        if (!stackable) maxStack = 1;
    }

    #region Properties

    public int ItemId => itemId;
    public string ItemName => itemName;
    public string Description => description;
    public Sprite Icon => icon;
    public ItemType ItemType => itemType;
    public bool Stackable => stackable;
    public int MaxStack => maxStack;

    public int SellValue => sellValue;

    #endregion


    public virtual bool Validate()
    {
        if (itemId < 0)
        {
            Debug.LogError($"[ItemDataSO] {name}: ItemId negatif olamaz!", this);
            return false;
        }

        if (string.IsNullOrWhiteSpace(itemName))
        {
            Debug.LogError($"[ItemDataSO] {name}: ItemName boş olamaz!", this);
            return false;
        }

        if (icon == null)
        {
            Debug.LogWarning($"[ItemDataSO] {name}: Icon atanmamış!", this);
        }

        return true;
    }
}
