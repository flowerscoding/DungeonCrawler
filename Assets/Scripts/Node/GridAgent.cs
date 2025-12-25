using UnityEngine;

public class GridAgent : MonoBehaviour
{
    public NodeClass node {get; private set;}
    public int nodeX;
    public int nodeY;
    void Start()
    {
        CreateStartNode();
    }
    void CreateStartNode()
    {

        //change to + if grid origin.x is positive. same is said for nodeY
        nodeX = Mathf.FloorToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x); 
        nodeY = Mathf.FloorToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);
        node = Node.instance.nodeGrid.grid[nodeX, nodeY];
    }
    public void SetNode(NodeClass newNode)
    {
        node = newNode;
        nodeX = node.indexX;
        nodeY = node.indexY;
    }
}
