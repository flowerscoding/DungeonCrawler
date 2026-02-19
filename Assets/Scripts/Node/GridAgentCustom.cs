using UnityEngine;

public class GridAgentCustom : MonoBehaviour
{
    public NodeClass.State nodeType;
    public int xLength;
    public int yLength;

    void Awake()
    {
        CreateStartNodes();
    }
    void CreateStartNodes()
    {
        //change to + if grid origin.x is positive. same is said for nodeY
        int nodeX = Mathf.RoundToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x);
        int nodeY = Mathf.RoundToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);
        NodeClass node = Node.instance.nodeGrid.grid[nodeX, nodeY];
        int length = xLength * yLength;
        for(int i = 0; i < xLength; i++)
        {
            
            for(int j = 0; j < yLength; i++)
            {
                
            }
        }
        node.state = nodeType;
        switch (nodeType)
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
}
