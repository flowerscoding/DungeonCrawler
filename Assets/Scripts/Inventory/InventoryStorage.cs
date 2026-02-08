using System.Collections.Generic;
using UnityEngine;

public class InventoryStorage : MonoBehaviour
{
    public List<ItemData> storage = new List<ItemData>();
    public List<ItemData> storageGeneralItems = new List<ItemData>();
    public List<ItemData> storageArmorItems = new List<ItemData>();
    public List<ItemData> storageKeyItems = new List<ItemData>();

    public void AddToInventory(ItemData itemData)
    {
        storage.Add(itemData);

        switch(itemData.ItemType)
        {
            case "Healing":
                storageGeneralItems.Add(itemData);
                break;
            default : break;
        }
    }
}
