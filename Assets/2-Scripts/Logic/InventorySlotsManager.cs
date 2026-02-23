using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsManager : MonoBehaviour
{
    public GameObject _slotPrefab;
    public List<InventorySlot> _slots = new List<InventorySlot>(6);

    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += DrawInventory;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= DrawInventory;
    }


    public void DrawInventory(List<InventoryItem> items)
    {

    }

}
