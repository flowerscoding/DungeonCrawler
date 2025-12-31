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
    }
    public void ResetNode(NodeClass node)
    {
        node.enemyController = null;
        node.boulderController = null;
        node.state = NodeClass.State.Empty;
    }
}
