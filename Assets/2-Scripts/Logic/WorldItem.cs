using UnityEngine;

public class WorldItem : MonoBehaviour
{

    [SerializeField]
    [Tooltip("Bu item'in veri referansı")]
    private ItemDataSO itemData;

    [SerializeField]
    [Range(1, 100)]
    [Tooltip("World'deki item stack sayısı")]
    private int stackCount = 1;

    private SpriteRenderer spriteRenderer;
    private bool isInitialized = false;

    #region Properties

    public ItemDataSO ItemData => itemData;
    public int StackCount => stackCount;

    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError($"[WorldItem] {gameObject.name}: SpriteRenderer gerekli!", this);
        }
    }

    private void Start()
    {
        if (itemData != null)
        {
            InitializeVisuals();
        }
    }

    /// <summary>
    /// ItemDataSO'dan sprite ve adı otomatik set et.
    /// </summary>
    private void InitializeVisuals()
    {
        if (itemData == null)
        {
            Debug.LogError($"[WorldItem] {gameObject.name}: ItemData null!");
            return;
        }

        // Sprite set et
        if (spriteRenderer != null && itemData.Icon != null)
        {
            spriteRenderer.sprite = itemData.Icon;
        }

        // GameObject adını set et
        gameObject.name = $"WorldItem_{itemData.ItemName}";

    }

   
}
