using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
    public InventoryNode[,] tab1Grid;
    public InventoryNode[,] tab2Grid;
    public InventoryNode[,] tab3Grid;
    public InventoryNode[,] activeGrid { get; private set; }
    public InventoryNode activeNode { get; private set; }
    public int gridXSize;
    public int gridYSize;
    void Awake()
    {
        tab1Grid = new InventoryNode[gridXSize, gridYSize];
        tab2Grid = new InventoryNode[gridXSize, gridYSize];
        tab3Grid = new InventoryNode[gridXSize, gridYSize];
        CreateGrid(tab1Grid);
        CreateGrid(tab2Grid);
        CreateGrid(tab3Grid);
        activeGrid = tab1Grid;
    }
    void CreateGrid(InventoryNode[,] grid)
    {
        for (int x = 0; x < gridXSize; x++)
        {
            for (int y = 0; y < gridYSize; y++)
            {
                grid[x, y] = new InventoryNode(x, y);
            }
        }
    }
    public void GridChange(int tabNumber)
    {
        activeGrid = tab1Grid;
        switch (tabNumber)
        {
            case 0:
                activeGrid = tab1Grid;
                break;
            case 1:
                activeGrid = tab2Grid;
                break;
            case 2:
                activeGrid = tab3Grid;
                break;
        }
        activeNode = activeGrid[0, 0];
    }
    public void AddItem(ItemData itemData)
    {
        switch(itemData.ItemType)
        {
            case "Healing": 
                foreach(InventoryNode node in tab1Grid)
                {
                    if(node.itemData == null)
                    {
                        node.itemData = itemData;
                        print("SUCCESSFULLY STORED");
                        break;
                    }
                    print("INVENTORY FULL");
                }
                break;
            default : break;
        }
    }
}
