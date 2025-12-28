using UnityEngine;

public class NodeClass
{
    public Vector3 worldPos;
    public int nodeX;
    public int nodeY;
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;

    public enum State
    {
        Empty,
        Unwalkable
    }
    public State state;
    public EnemyController occupant;
    public NodeClass(Vector3 _worldPos, int _nodeX, int _nodeY)
    {
        worldPos = _worldPos;
        nodeX = _nodeX;
        nodeY = _nodeY;
    }
}
