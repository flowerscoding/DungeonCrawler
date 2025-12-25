using UnityEngine;

public class NodeClass
{
    public Vector3 worldPos;
    public int indexX;
    public int indexY;
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;
    public enum State
    {
        Walkable,
        Unwalkable
    }
    public State state;
    public NodeClass(Vector3 _worldPos, int _indexX, int _indexY)
    {
        worldPos = _worldPos;
        indexX = _indexX;
        indexY = _indexY;
    }
}
