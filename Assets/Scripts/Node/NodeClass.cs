using UnityEngine;

public class NodeClass
{
    public Vector3 worldPos;
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;
    public NodeClass(Vector3 _worldPos)
    {
        worldPos = _worldPos;
    }
}
