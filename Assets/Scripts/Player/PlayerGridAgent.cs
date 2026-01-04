using UnityEngine;

public class PlayerGridAgent : MonoBehaviour
{
    public static event System.Action<NodeClass> OnPlayerMovement;
    public NodeClass.State state;
    public NodeClass node { get; private set; }
    public int nodeX;
    public int nodeY;
    public void SetStartNode(NodeClass newNode)
    {
        node = newNode;
        nodeX = node.nodeX;
        nodeY = node.nodeY;

        node.state = NodeClass.State.Player;

        node.enemyController = null;
        node.boulderController = null;
        node.ladderController = null;

        OnPlayerMovement?.Invoke(node);
    }
    public void SetNode(NodeClass newNode)
    {
        node.state = NodeClass.State.Empty;
        newNode.state = node.state;

        node.enemyController = null;
        node.boulderController = null;
        node.ladderController = null;

        node = newNode;
        nodeX = node.nodeX;
        nodeY = node.nodeY;
        switch (state)
        {
            case NodeClass.State.Player:
                node.state = NodeClass.State.Player;
                break;
        }

        OnPlayerMovement?.Invoke(node);
    }
}