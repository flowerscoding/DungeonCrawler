using System.Collections.Generic;
using UnityEngine;

public class InventoryStorage : MonoBehaviour
{
    public List<ItemData> storage = new List<ItemData>();
    public void AddToInventory(ItemData itemData)
    {
        storage.Add(itemData);
        Inventory.Instance.AddItemToGrid(itemData);
    }
}
