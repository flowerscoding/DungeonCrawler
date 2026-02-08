using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public InventoryGrid grid;
    public InventoryBoxState inventoryBoxState;
    
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
    public void ActiveGridChange(int tabNumber)
    {
        grid.GridChange(tabNumber);
    }
    public void AddItemToGrid(ItemData itemData)
    {
        grid.AddItem(itemData);
    }
}
