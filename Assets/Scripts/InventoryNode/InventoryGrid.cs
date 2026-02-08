using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    public InventoryNode[,] grid;
    public InventoryNode[,] activeGrid { get; private set; }
    public int gridXSize;
    public int gridYSize;
    void Awake()
    {
        grid = new InventoryNode[gridXSize, gridYSize];
        CreateGrid(grid);
        activeGrid = grid;
    }
    void CreateGrid(InventoryNode[,] grid)
    {
        for (int y = 0; y < gridXSize; y++)
        {
            for (int x = 0; x < gridYSize; x++)
            {
                grid[x, y] = new InventoryNode(x, y);
            }
        }
    }
    void ResetGrid()
    {
        for (int y = 0; y < gridYSize; y++)
        {
            for (int x = 0; x < gridXSize; x++)
            {
                grid[x, y] = new InventoryNode(x, y);
            }
        }
    }
    public void SetGrid(string itemsType)
    {
        ResetGrid();
        switch (itemsType)
        {
            case "General":
                foreach (ItemData itemData in Inventory.Instance.inventoryStorage.storageGeneralItems)
                {
                    foreach (InventoryNode node in grid)
                        node.itemData = itemData;
                }
                break;
            case "Armor":
                foreach (ItemData itemData in Inventory.Instance.inventoryStorage.storageArmorItems)
                {
                    foreach (InventoryNode node in grid)
                        node.itemData = itemData;
                }
                break;
            case "Key":
                foreach (ItemData itemData in Inventory.Instance.inventoryStorage.storageKeyItems)
                {
                    foreach (InventoryNode node in grid)
                        node.itemData = itemData;
                }
                break;
            default: break;
        }
    }
}
