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
        if(transform.TryGetComponent(out EnemyController enemyController))
        {
            node.occupant = enemyController;
        }
    }
    public void SetNode(NodeClass newNode)
    {
        node.state = NodeClass.State.Empty;
        node.occupant = null;
        node = newNode;
        nodeX = node.nodeX;
        nodeY = node.nodeY;
        node.state = NodeClass.State.Unwalkable;
        if(transform.TryGetComponent(out EnemyController enemyController))
        {
            node.occupant = enemyController;
        }
    }
}
