using UnityEngine;
using System;

[Serializable]
public class InventoryItem
{
    public ItemDataSO _itemData;
    public int _stackSize;

    public InventoryItem(ItemDataSO item)
    {
        _itemData = item;
        AddToStack();
    }

    public void AddToStack()
    {
        _stackSize++;
    }
    public void RemoveFromStack()
    {
        _stackSize--;
    }
}
