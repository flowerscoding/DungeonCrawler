using UnityEngine;

public class GridAgent : MonoBehaviour
{
    public NodeClass.State state;
    public NodeClass node { get; private set; }
    public int nodeX;
    public int nodeY;
    void Awake()
    {
        CreateStartNode();
    }
    void CreateStartNode()
    {
        //change to + if grid origin.x is positive. same is said for nodeY
        nodeX = Mathf.RoundToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x);
        nodeY = Mathf.RoundToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);
        node = Node.instance.nodeGrid.grid[nodeX, nodeY];
        node.state = state;
        switch (state)
        {
            case NodeClass.State.Enemy:
                node.enemyController = transform.GetComponent<EnemyController>();
                break;
            case NodeClass.State.Boulder:
                node.boulderController = transform.GetComponent<BoulderController>();
                break;
            case NodeClass.State.Chest:
                break;
            case NodeClass.State.Ladder:
                node.ladderController = transform.GetComponent<LadderController>();
                break;
            case NodeClass.State.Destructible:
                node.destructibleController = transform.GetComponent<DestructibleController>();
                break;
            case NodeClass.State.Coffin:
                node.coffinController = GetComponent<CoffinController>();
                break;
            case NodeClass.State.Queen:
                node.npcController = GetComponent<NpcController>();
                break;
            case NodeClass.State.TransportDoor:
                node.transportController = GetComponent<TransportController>();
                break;
            case NodeClass.State.Item:
                node.itemController = GetComponent<ItemController>();
                break;
        }
    }
    public void SetNode(NodeClass newNode)
    {
        newNode.state = node.state;
        node.state = NodeClass.State.Empty;

        node.enemyController = null;
        node.boulderController = null;
        node.ladderController = null;
        node.itemController = null;
        node.destructibleController = null;

        node = newNode;
        nodeX = node.nodeX;
        nodeY = node.nodeY;
        switch (state)
        {
            case NodeClass.State.Enemy:
                node.enemyController = GetComponent<EnemyController>();
                node.state = NodeClass.State.Enemy;
                break;
            case NodeClass.State.Boulder:
                node.boulderController = GetComponent<BoulderController>();
                node.state = NodeClass.State.Boulder;
                break;
            case NodeClass.State.Chest:
                break;
            case NodeClass.State.Ladder:
                node.ladderController = GetComponent<LadderController>();
                node.state = NodeClass.State.Ladder;
                break;
            case NodeClass.State.Destructible:
                node.destructibleController = GetComponent<DestructibleController>();
                node.state = NodeClass.State.Destructible;
                break;
            case NodeClass.State.Queen:
                node.npcController = GetComponent<NpcController>();
                node.state = NodeClass.State.Queen;
                break;
            case NodeClass.State.TransportDoor:
                node.transportController = GetComponent<TransportController>();
                node.state = NodeClass.State.TransportDoor;
                break;
            case NodeClass.State.Item:
                node.itemController = GetComponent<ItemController>();
                node.state = NodeClass.State.Item;
                break;
        }
    }
    public void ChangeNodeState(NodeClass.State newState) //for destructibles
    {
        node.state = newState;
    }
}
