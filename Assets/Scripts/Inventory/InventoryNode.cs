using UnityEngine;

public class InventoryNode
{
    public int xIndex;
    public int yIndex;
    public ItemData itemData;
    public InventoryNode(int _xIndex, int _yIndex)
    {
        xIndex = _xIndex;
        yIndex = _yIndex;
    }
}
