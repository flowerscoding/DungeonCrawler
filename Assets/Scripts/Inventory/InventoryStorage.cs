using System.Collections.Generic;
using UnityEngine;

public class InventoryStorage : MonoBehaviour
{
    public List<ItemData> storage = new List<ItemData>();
    public List<ItemData> storageConsumableItems = new List<ItemData>();
    public List<ItemData> storageArmorItems = new List<ItemData>();
    public List<ItemData> storageKeyItems = new List<ItemData>();

    public void AddToInventory(ItemData itemData)
    {
        storage.Add(itemData);

        switch(itemData.ItemType)
        {
            case "Consumable":
                storageConsumableItems.Add(itemData);
                break;
            default : break;
        }
        SaveSystem.Instance.SaveGame();
    }
    public void RemoveFromInventory(ItemData itemData)
    {
        storage.Add(itemData);

        switch(itemData.ItemType)
        {
            case "Consumable":
                storageConsumableItems.Remove(itemData);
                break;
            default : break;
        }
        SaveSystem.Instance.SaveGame();
    }
    public void LoadInventory()
    {
        storage = SaveSystem.Instance.currentData.storage;
        storageConsumableItems = SaveSystem.Instance.currentData.storageConsumableItems;
        storageArmorItems = SaveSystem.Instance.currentData.storageArmorItems;
        storageKeyItems = SaveSystem.Instance.currentData.storageKeyItems;
    }
}
