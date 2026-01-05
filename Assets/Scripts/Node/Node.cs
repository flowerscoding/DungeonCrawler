using UnityEngine;

public class Node : MonoBehaviour
{
    public static Node instance;
    public NodeGrid nodeGrid;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        AssignNodes();
    }
    public void ResetNode(NodeClass node)
    {
        node.enemyController = null;
        node.boulderController = null;
        node.ladderController = null;
        node.destructibleController = null;
        node.state = NodeClass.State.Empty;
    }
    public void AssignNodes()
    {
        nodeGrid.CreateGrid();
        nodeGrid.AssignPlayer();
    }
}
