using UnityEngine;

public class Node : MonoBehaviour
{
    public static Node instance;
    public NodeGrid nodeGrid;
    public NodeVisibility[] nodeVisibilityScripts;
    public NodeVisibilityMultiple[] nodeVisibilityMultiplesScripts;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        AssignNodes();
        GrabScripts();
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
    }
    void GrabScripts()
    {
        nodeVisibilityScripts = FindObjectsByType<NodeVisibility>(FindObjectsSortMode.None);
        nodeVisibilityMultiplesScripts = FindObjectsByType<NodeVisibilityMultiple>(FindObjectsSortMode.None);
    }
    public void DistanceCheck(NodeClass playerNode)
    {
        foreach(NodeVisibility script in nodeVisibilityScripts)
            script.DistanceCheck(playerNode);
        foreach(NodeVisibilityMultiple script in nodeVisibilityMultiplesScripts)
            script.DistanceCheck(playerNode);
    }
}
