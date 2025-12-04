using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData itemData;   
    public int quantity;        
}

public class Inventory : MonoBehaviour
{
    [Header("Inventory Slots")]
    public List<InventorySlot> items = new List<InventorySlot>();

    
    public event Action OnInventoryChanged;

    
    public void AddItem(ItemData itemData, int amount)
    {
        if (itemData == null || amount <= 0) return;

        
        InventorySlot slot = items.Find(s => s.itemData == itemData);
        if (slot != null)
        {
            slot.quantity += amount;
            Debug.Log($"Added {amount} x {itemData.itemName}. New quantity: {slot.quantity}");
        }
        else
        {
            items.Add(new InventorySlot { itemData = itemData, quantity = amount });
            Debug.Log($"Added new item: {itemData.itemName} x{amount}");
        }

        
        OnInventoryChanged?.Invoke();
    }

    public void RemoveItem(ItemData itemData, int amount)
    {
        InventorySlot slot = items.Find(s => s.itemData == itemData);
        if (slot != null)
        {
            slot.quantity -= amount;
            if (slot.quantity <= 0)
                items.Remove(slot);

            
            OnInventoryChanged?.Invoke();
        }
    }

        public int GetQuantity(ItemData itemData)
    {
        InventorySlot slot = items.Find(s => s.itemData == itemData);
        return slot != null ? slot.quantity : 0;
    }
}

