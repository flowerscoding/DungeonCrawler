using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveDuration;
    public float turnDuration;
    public Rigidbody playerRB;

    private InputAction _upAction;
    private InputAction _downAction;
    private InputAction _leftAction;
    private InputAction _rightAction;
    private string _holdingDirection;
    void Awake()
    {
        _upAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Up");
        _downAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Down");
        _leftAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Left");
        _rightAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Right");
    }
    void OnEnable()
    {
        _upAction.performed += MoveUp;
        _downAction.performed += MoveDown;
        _leftAction.performed += MoveLeft;
        _rightAction.performed += MoveRight;

        _upAction.canceled += UpRelease;
        _downAction.canceled += DownRelease;
        _leftAction.canceled += LeftRelease;
        _rightAction.canceled += RightRelease;
    }
    void UpRelease(InputAction.CallbackContext ctx)
    {
        if(_holdingDirection == "up")
            _holdingDirection = " ";
    }
    void DownRelease(InputAction.CallbackContext ctx)
    {
        if(_holdingDirection == "down")
            _holdingDirection = " ";
    }
    void LeftRelease(InputAction.CallbackContext ctx)
    {
        if(_holdingDirection == "left")
            _holdingDirection = " ";
    }
    void RightRelease(InputAction.CallbackContext ctx)
    {
        if(_holdingDirection == "right")
            _holdingDirection = " ";
    }
    void MoveUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            MoveDirection("up");
    }
    void MoveDown(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            MoveDirection("down");
    }
    void MoveLeft(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            MoveDirection("left");
    }
    void MoveRight(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            MoveDirection("right");
    }
    bool CheckIfWalkable(NodeClass targetNode)
    {
        if (targetNode.state != NodeClass.State.Empty)
        {
            print("blocked");
            PlayCrashAnimation(targetNode);
            return false;
        }
        return true;
    }
    void PlayCrashAnimation(NodeClass targetNode) //like pokemon's walk animation towards a wall. maybe add sfx
    {
        Vector3 direction = targetNode.worldPos - playerRB.transform.position;
        playerRB.transform.forward = direction;
        return;
    }
    void MoveDirection(string direction)
    {
        if (TurnManager.instance.state != TurnManager.State.PlayerTurn) return;
        _holdingDirection = direction;
        int nodeX = 0;
        int nodeY = 0;
        switch (direction)
        {
            case "up": nodeX = 0; nodeY = 1; break;
            case "down": nodeX = 0; nodeY = -1; break;
            case "left": nodeX = -1; nodeY = 0; break;
            case "right": nodeX = 1; nodeY = 0; break;
        }
        nodeX += Player.instance.gridAgent.nodeX;
        nodeY += Player.instance.gridAgent.nodeY;
        NodeClass targetNode = Node.instance.nodeGrid.grid[nodeX, nodeY];

        if (!CheckIfWalkable(targetNode))
            return; //return if empty. shouldn't waste a player's turn

        Player.instance.gridAgent.SetNode(targetNode);

        StartCoroutine(MoveToTarget(targetNode));

        Enemy.instance.enemySystem.MoveAI();
    }
    IEnumerator MoveToTarget(NodeClass targetNode)
    {
        Player.instance.StateChange(PlayerState.State.Walking);
        Vector3 direction = targetNode.worldPos - playerRB.transform.position;
        playerRB.transform.forward = direction;
        TurnManager.instance.ChangeTurn(TurnManager.State.EnemyTurn);
        Vector3 start = playerRB.position;
        Vector3 goal = targetNode.worldPos;
        Quaternion goalQuat = Quaternion.LookRotation(goal - start);
        Quaternion startQuat = playerRB.rotation;
        float t = 0;
        float t2 = 0;
        while (t < 1)
        {
            if(t2 < 1)
            {
                t2 += Time.fixedDeltaTime / turnDuration;
                playerRB.MoveRotation(Quaternion.Slerp(startQuat, goalQuat, t2));
            }
            else
                playerRB.rotation = goalQuat;
            t += Time.fixedDeltaTime / moveDuration;
            playerRB.MovePosition(Vector3.Lerp(start, goal, t));
            yield return new WaitForFixedUpdate();
        }
        if (t >= 1)
        {
            playerRB.position = goal;
            playerRB.rotation = goalQuat;
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            if (_holdingDirection != " " && TurnManager.instance.state == TurnManager.State.PlayerTurn)
                switch (_holdingDirection)
                {
                    case "up": MoveDirection("up"); break;
                    case "down": MoveDirection("down"); break;
                    case "left": MoveDirection("left"); break;
                    case "right": MoveDirection("right"); break;
                }
            else
                Player.instance.StateChange(PlayerState.State.Idle);

        }
    }
}
