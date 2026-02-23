using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [Header("UI Refference")]
    [SerializeField] private TextMeshProUGUI _itemLabel;
    [SerializeField] private TextMeshProUGUI _stackSize;
    [SerializeField] private Image _itemIcon;

    public void DrawSlots(InventoryItem item)
    {
        if (item == null)
        {
            ClearSlots();
            return;
        }

        _itemIcon.enabled = true;
        _stackSize.enabled = true;
        _itemIcon.enabled = true;

        _itemIcon.sprite = item._itemData.itemIcon;
        _itemLabel.text = item._itemData.itemName;
        _stackSize.text = item._stackSize.ToString();
    }

    public void ClearSlots()
    {
        _itemIcon.enabled = false;
        _stackSize.enabled = false;
        _itemIcon.enabled = false;
    }




}
