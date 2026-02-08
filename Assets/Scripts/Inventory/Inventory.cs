using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public InventoryGrid grid;
    public InventoryBoxState inventoryBoxState;
    public InventoryStorage inventoryStorage;
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    public void XChange(int direction)
    {
        inventoryBoxState.XChange(direction);
    }
    public void YChange(int direction)
    {
        inventoryBoxState.YChange(direction);
    }
    public void TabChange(int direction)
    {
        inventoryBoxState.TabChange(direction);
    }
    public void InventoryToggled()
    {
        inventoryBoxState.InventoryToggled();
    }
    public void AddItemToInventory(ItemData itemData)
    {
        inventoryStorage.AddToInventory(itemData);
    }
    public void SetGrid(string itemsType)
    {
        grid.SetGrid(itemsType);
    }
}
