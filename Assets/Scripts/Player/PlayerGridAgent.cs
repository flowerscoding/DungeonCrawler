using UnityEngine;

public class PlayerGridAgent : MonoBehaviour
{
    public NodeClass.State state;
    public NodeClass node { get; private set; }
    public int nodeX;
    public int nodeY;
    public void CreateStartNode()
    {
        //change to + if grid origin.x is positive. same is said for nodeY
        nodeX = Mathf.FloorToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x);
        nodeY = Mathf.FloorToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);
        node = Node.instance.nodeGrid.grid[nodeX, nodeY];
    }
    public void SetNode(NodeClass newNode)
    {
        newNode.state = node.state;
        node.state = NodeClass.State.Empty;

        node.enemyController = null;
        node.boulderController = null;
        node.latterController = null;

        node = newNode;
        nodeX = node.nodeX;
        nodeY = node.nodeY;
        switch (state)
        {
            case NodeClass.State.Player:
                node.state = NodeClass.State.Player;
                break;
        }
    }
}