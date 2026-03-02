using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public static event Action<List<InventoryItem>> OnInventoryChanged;

    public List<InventoryItem> _inventory = new List<InventoryItem>();
    public Dictionary<ItemDataSO, InventoryItem> _itemDictionary = new Dictionary<ItemDataSO, InventoryItem>();

    
    void Start()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

        }
    }

    public void AddItem(ItemDataSO item)
    {
        if (_itemDictionary.ContainsKey(item))
        {
            _itemDictionary[item].AddToStack();
            Debug.Log($"Added {item.ItemName} to inventory. Stack size: {_itemDictionary[item]._stackSize}");
            OnInventoryChanged?.Invoke(_inventory);
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            _inventory.Add(newItem);
            _itemDictionary.Add(item, newItem);
            Debug.Log($"Did not find the item but added {item.ItemName} to inventory. Stack size: {newItem._stackSize}");
            OnInventoryChanged?.Invoke(_inventory);
        }
    }

    public void RemoveItem(ItemDataSO item)
    {
        if (_itemDictionary.ContainsKey(item))
        {
            InventoryItem inventoryItem = _itemDictionary[item];
            inventoryItem.RemoveFromStack();
            Debug.Log($"Removed {item.ItemName} from inventory. Stack size: {inventoryItem._stackSize}");
            if (inventoryItem._stackSize <= 0)
            {
                _inventory.Remove(inventoryItem);
                _itemDictionary.Remove(item);
                Debug.Log($"{item.ItemName} stack is empty and removed from inventory.");
            }
            OnInventoryChanged?.Invoke(_inventory);
        }
        else
        {
            Debug.LogWarning($"Attempted to remove {item.ItemName} but it was not found in inventory.");
        }
    }

    private void OnEnable()
    {
        CollectItems.onItemCollected += AddItem;
    }
    private void OnDisable()
    {
        CollectItems.onItemCollected -= AddItem;
    }
}
