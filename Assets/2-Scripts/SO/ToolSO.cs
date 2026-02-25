using UnityEngine;


[CreateAssetMenu(fileName = "ToolSO", menuName = "Scriptable Objects/ToolSO")]
/// <summary>
/// Tarım/mining araçları için item türü.
/// Durability ve power özelliği içerir.
/// </summary>
public class ToolSO : ItemDataSO
{
    [CreateAssetMenu(fileName = "ToolItem_", menuName = "Inventory/Items/Tool Item")]
    public class ToolItemSO : ItemDataSO
    {
        [SerializeField]
        [Tooltip("Tool türü (Hoe, Axe, vb.)")]
        private ToolType toolType;

        [SerializeField]
        [Range(1, 1000)]
        [Tooltip("Maksimum dayanıklılık")]
        private int maxDurability = 100;

        [SerializeField]
        [Range(1, 100)]
        [Tooltip("Araç gücü/hasar")]
        private int power = 10;

        #region Properties

        public ToolType ToolType => toolType;
        public int MaxDurability => maxDurability;
        public int Power => power;

        #endregion

        public override bool Validate()
        {
            if (!base.Validate()) return false;

            if (maxDurability <= 0)
            {
                Debug.LogError($"[ToolItemSO] {name}: MaxDurability 0'dan büyük olmalı!", this);
                return false;
            }

            if (power <= 0)
            {
                Debug.LogError($"[ToolItemSO] {name}: Power 0'dan büyük olmalı!", this);
                return false;
            }

            return true;
        }
    }
}
