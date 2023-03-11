#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class Inventory : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();
    public UnityEvent<List<ItemData>> inventoryUpdated;

    public void AddItem(ItemData item)
    {
        items.Add(item);
        Debug.Log($"Added {item.itemType} to inventory. Inventory has {items.Count} items");
        inventoryUpdated.Invoke(items);
    }

    public void RemoveItem(ItemData item)
    {
        items.Remove(item);
        Debug.Log($"Removed{item.itemType} to inventory. Inventory has {items.Count} items");
        inventoryUpdated.Invoke(items);
    }

    public ItemData? GetItem()
    {
        return items.FirstOrDefault();
    }
}
