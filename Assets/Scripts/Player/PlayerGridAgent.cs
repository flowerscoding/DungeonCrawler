using UnityEngine;

public class PlayerGridAgent : MonoBehaviour
{
    public NodeClass.State state;
    public NodeClass node { get; private set; }
    public int nodeX;
    public int nodeY;
    public void SetStartNode(NodeClass newNode)
    {
        node = newNode;
        node.state = NodeClass.State.Player;

        node.enemyController = null;
        node.boulderController = null;
        node.ladderController = null;
    }
    public void SetNode(NodeClass newNode)
    {
        newNode.state = node.state;
        node.state = NodeClass.State.Empty;

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
    }
}