using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public Canvas actionsUI;
    public NodeClass curInteractingNode;
    public NodeClass goalNode;

    public InputAction interactAction;
    void Awake()
    {
        interactAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Interact");
    }
    void OnEnable()
    {
        interactAction.performed += InteractPressed;
    }
    void InteractPressed(InputAction.CallbackContext ctx)
    {
        if (curInteractingNode == null 
        || TurnManager.instance.state != TurnManager.State.PlayerTurn 
        || !ctx.performed) return;

        switch (curInteractingNode.state)
        {
            case NodeClass.State.Enemy:
            EnemyInteract();
                break;
            case NodeClass.State.Boulder:
                BoulderPush();
                break;
            case NodeClass.State.Chest:

                break;
        }
    }
    void EnemyInteract()
    {
        ActionsMenu.instance.EnableActionsMenu();
    }
    void BoulderPush()
    {
        BoulderPushCheck();
    }
    void BoulderPushCheck()
    {
        Vector3 direction = curInteractingNode.worldPos - Player.instance.gridAgent.node.worldPos;
        int goalX;
        int goalY;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.z))
        {
            if (direction.x > 0)
            {
                goalX = curInteractingNode.nodeX + 1;
                goalY = curInteractingNode.nodeY;
            }
            else
            {
                goalX = curInteractingNode.nodeX - 1;
                goalY = curInteractingNode.nodeY;
            }
        }
        else
        {
            if (direction.z > 0)
            {
                goalX = curInteractingNode.nodeX;
                goalY = curInteractingNode.nodeY + 1;
            }
            else
            {
                goalX = curInteractingNode.nodeX;
                goalY = curInteractingNode.nodeY - 1;
            }
        }
        if (Node.instance.nodeGrid.grid[goalX, goalY] != null)
        {
            goalNode = Node.instance.nodeGrid.grid[goalX, goalY];
            TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
            if (goalNode.state == NodeClass.State.Empty)
            {
                curInteractingNode.boulderController.NewState(BoulderState.State.Moving);
                Player.instance.StateChange(PlayerState.State.BoulderPush);
                Player.instance.MoveBoulder(curInteractingNode);
                Player.instance.gridAgent.SetNode(curInteractingNode);
            }
            else
            {
                Player.instance.StateChange(PlayerState.State.PushFail);
            }
        }
    }
    public void InteractablesOff()
    {
        if (curInteractingNode != null)
            switch (curInteractingNode.state)
            {
                case NodeClass.State.Boulder:
                    curInteractingNode.boulderController.NewState(BoulderState.State.Idle);
                    break;
            }
    }
    public void CheckInteractables()
    {
        NodeClass playerNode = Player.instance.gridAgent.node;
        NodeClass checkingNode;
        NodeClass[,] grid = Node.instance.nodeGrid.grid;
        switch (Player.instance.playerMovement.directionState)
        {
            case PlayerMovement.DirectionState.Up:
                checkingNode = grid[playerNode.nodeX, playerNode.nodeY + 1];
                break;
            case PlayerMovement.DirectionState.Down:
                checkingNode = grid[playerNode.nodeX, playerNode.nodeY - 1];
                break;
            case PlayerMovement.DirectionState.Left:
                checkingNode = grid[playerNode.nodeX - 1, playerNode.nodeY];
                break;
            case PlayerMovement.DirectionState.Right:
                checkingNode = grid[playerNode.nodeX + 1, playerNode.nodeY];
                break;
            default: checkingNode = grid[playerNode.nodeX, playerNode.nodeY]; break;
        }
        if (checkingNode != null)
        {
            curInteractingNode = checkingNode;
            switch (checkingNode.state)
            {
                case NodeClass.State.Empty: break;
                case NodeClass.State.Enemy: break;
                case NodeClass.State.Boulder: BoulderInteractable(checkingNode); break;
            }
        }
    }
    void BoulderInteractable(NodeClass boulderNode)
    {
        boulderNode.boulderController.NewState(BoulderState.State.Interactable);
    }
    void EnemyInteractable()
    {

    }
}
