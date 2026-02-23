using System;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsManager : MonoBehaviour
{
    [Header("Variebles")]
    public GameObject _slotPrefab;
    public List<InventorySlot> _slots = new List<InventorySlot>(6);

    public void DrawInventory(List<InventoryItem> inventory)
    {
        ResetInventory();


        for (int i = 0; i < _slots.Capacity; i++)
        {
            CreateInventorySlot();
        }

        for (int i = 0; i < inventory.Count; i++)
        {
            _slots[i].DrawSlots(inventory[i]);
        }

    }
    void ResetInventory()
    {
        foreach (Transform childTransform in transform)
        {
            Destroy(childTransform.gameObject);
        }
        _slots = new List<InventorySlot>(6);
    }

    void CreateInventorySlot()
    {
        GameObject newSlot = Instantiate(_slotPrefab);
        newSlot.transform.SetParent(transform, false);

        InventorySlot newSlotComponent = newSlot.GetComponent<InventorySlot>();
        newSlotComponent.ClearSlots();
        _slots.Add(newSlotComponent);
    }


    private void OnEnable()
    {
        InventoryManager.OnInventoryChanged += DrawInventory;
    }

    private void OnDisable()
    {
        InventoryManager.OnInventoryChanged -= DrawInventory;
    }

}
