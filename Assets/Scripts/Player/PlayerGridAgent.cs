using System;
using UnityEngine;

public class PlayerGridAgent : MonoBehaviour
{
    public static event Action<NodeClass> OnPlayerMovement;
    public NodeClass.State state;
    public NodeClass node { get; private set; }
    public int nodeX;
    public int nodeY;
    public void InitializeStartNode(NodeClass node)
    {
        SetStartNode(node);
    }
    public void SetStartNode(NodeClass newNode)
    {
        node = newNode;
        nodeX = node.nodeX;
        nodeY = node.nodeY;

        Rigidbody rb = Player.instance.playerMovement.playerRB;
        rb.position = node.worldPos;

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