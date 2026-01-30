using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    public NodeClass curInteractingNode;
    public NodeClass goalNode;

    private InputAction _interactAction;
    [SerializeField] 
    void Awake()
    {
        _interactAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Interact");
    }
    void OnEnable()
    {
        _interactAction.performed += InteractPressed;
    }
    void OnDisable()
    {
        _interactAction.performed -= InteractPressed;
    }
    void InteractPressed(InputAction.CallbackContext ctx)
    {

        InputManager.instance.ControllerTypeCheck(ctx);

        if (curInteractingNode == null
        || TurnManager.instance.state != TurnManager.State.PlayerTurn
        || !ctx.performed) return;

        if (curInteractingNode.enemyController != null && curInteractingNode.enemyController.enemyState.state == EnemyState.State.Dead)
            return; //dont interact with dead enemies

        switch (curInteractingNode.state)
        {
            case NodeClass.State.Enemy:
                EnemyInteract();
                break;
            case NodeClass.State.Boulder:
                BoulderPushCheck();
                break;
            case NodeClass.State.Chest:

                break;
            case NodeClass.State.Ladder:
                LadderInteract();
                break;
            case NodeClass.State.Destructible:
                DestructibleInteract();
                break;
            case NodeClass.State.Coffin:
            CoffinInteract();
                break;
            case NodeClass.State.Queen:
            QueenInteract();
                break;
        }
    }
    public DialogueLines queenLines;
    public DialogueLines playerLines;
    void QueenInteract()
    {
        if(TurnManager.instance.state == TurnManager.State.Dialogue) return;

        print(3);
        TurnManager.instance.ChangeTurn(TurnManager.State.Dialogue);
        Player.instance.StateChange(PlayerState.State.Dialogue);
        DialogueSystem.instance.RunDialogue();
    }
    void CoffinInteract()
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
        Player.instance.StateChange(PlayerState.State.Pray);
        InputManager.instance.MapChange(InputMapping.MapType.RestMenu);
    }
    void DestructibleInteract()
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
        Player.instance.StateChange(PlayerState.State.Destroy);
    }
    void LadderInteract()
    {
        if (curInteractingNode.ladderController.active)
        {
            Player.instance.Climb(curInteractingNode);
            curInteractingNode.ladderController.DeactivateLadder();
        }
    }
    void EnemyInteract()
    {
        ActionsMenu.instance.EnableActionsMenu();
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
                case NodeClass.State.Ladder:
                    curInteractingNode.ladderController.DeactivateLadder();
                    break;
                case NodeClass.State.Coffin:
                    curInteractingNode.coffinController.InteractablesOff();
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
                case NodeClass.State.Ladder: LadderInteractable(checkingNode); break;
                case NodeClass.State.Coffin: CoffinInteractable(checkingNode); break;
            }
        }
    }
    void CoffinInteractable(NodeClass coffinNode)
    {
        coffinNode.coffinController.InteractablesOn();
    }
    void LadderInteractable(NodeClass stairNode)
    {
        stairNode.ladderController.ActivateLadder();
    }
    void BoulderInteractable(NodeClass boulderNode)
    {
        boulderNode.boulderController.NewState(BoulderState.State.Interactable);
    }
}
