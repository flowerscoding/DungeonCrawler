using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRB;

    public enum DirectionState
    {
        Up,
        Down,
        Left,
        Right
    }
    public DirectionState directionState;
    private InputAction _leftStickAction;
    private InputAction _upAction;
    private InputAction _downAction;
    private InputAction _leftAction;
    private InputAction _rightAction;
    private InputAction _runToggleAction;
    private string _holdingDirection;
    public bool runToggle;
    void Awake()
    {
        _upAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Up");
        _downAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Down");
        _leftAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Left");
        _rightAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Right");
        _runToggleAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("RunToggle");
        _leftStickAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("LStick");
    }
    void OnEnable()
    {
        _upAction.performed += MoveUp;
        _downAction.performed += MoveDown;
        _leftAction.performed += MoveLeft;
        _rightAction.performed += MoveRight;
        _runToggleAction.performed += RunToggle;
        _leftStickAction.performed += StickMovement;

        _leftStickAction.canceled += StickMovement;
        _runToggleAction.canceled += RunUntoggle;
        _upAction.canceled += UpRelease;
        _downAction.canceled += DownRelease;
        _leftAction.canceled += LeftRelease;
        _rightAction.canceled += RightRelease;
    }
    void ControllerTypeCheck(InputAction.CallbackContext ctx)
    {
        InputManager.instance.ControllerTypeCheck(ctx);
    }
    void StickMovement(InputAction.CallbackContext ctx)
    {
        ControllerTypeCheck(ctx);
        Vector2 input = ctx.ReadValue<Vector2>();
        if (input.magnitude < 0.01f)
        {
            _holdingDirection = " ";
            return;
        }
        bool xDir = Mathf.Abs(input.x) > Mathf.Abs(input.y);
        if (xDir)
        {
            MoveDirection(input.x > 0 ? "right" : "left");
        }
        else
            MoveDirection(input.y > 0 ? "up" : "down");
    }
    void RunToggle(InputAction.CallbackContext ctx)
    {
        runToggle = true;
    }
    void RunUntoggle(InputAction.CallbackContext ctx)
    {
        runToggle = false;
    }
    void UpRelease(InputAction.CallbackContext ctx)
    {
        if (_holdingDirection == "up")
            _holdingDirection = " ";
    }
    void DownRelease(InputAction.CallbackContext ctx)
    {
        if (_holdingDirection == "down")
            _holdingDirection = " ";
    }
    void LeftRelease(InputAction.CallbackContext ctx)
    {
        if (_holdingDirection == "left")
            _holdingDirection = " ";
    }
    void RightRelease(InputAction.CallbackContext ctx)
    {
        if (_holdingDirection == "right")
            _holdingDirection = " ";
    }
    void MoveUp(InputAction.CallbackContext ctx)
    {
        ControllerTypeCheck(ctx);
        if (ctx.performed)
            MoveDirection("up");
    }
    void MoveDown(InputAction.CallbackContext ctx)
    {
        ControllerTypeCheck(ctx);
        if (ctx.performed)
            MoveDirection("down");
    }
    void MoveLeft(InputAction.CallbackContext ctx)
    {
        ControllerTypeCheck(ctx);
        if (ctx.performed)
            MoveDirection("left");
    }
    void MoveRight(InputAction.CallbackContext ctx)
    {
        ControllerTypeCheck(ctx);
        if (ctx.performed)
            MoveDirection("right");
    }
    bool CheckIfWalkable(NodeClass targetNode)
    {
        if (targetNode.state != NodeClass.State.Empty
        && targetNode.state != NodeClass.State.TransportDoor)
        {
            StartCoroutine(TrackCrashAnimation(targetNode));
            return false;
        }
        return true;
    }
    bool CheckTransport(NodeClass targetNode)
    {
        if (targetNode.state == NodeClass.State.TransportDoor)
            return true;
        return false;
    }
    IEnumerator TrackCrashAnimation(NodeClass targetNode)
    {
        Player.instance.StateChange(PlayerState.State.Walking);

        Quaternion start = playerRB.rotation;
        Quaternion goal = Quaternion.LookRotation(targetNode.worldPos - playerRB.position);
        float t = 0;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / TurnManager.instance.rotateDuration;
            playerRB.MoveRotation(Quaternion.Slerp(start, goal, t));
            yield return new WaitForFixedUpdate();
        }
        if (t >= 1 && Player.instance.playerState.state == PlayerState.State.Walking)
            Player.instance.playerState.NewState(PlayerState.State.Idle);
    }
    void MoveDirection(string direction)
    {
        _holdingDirection = direction;
        if (TurnManager.instance.state != TurnManager.State.PlayerTurn) return;
        int nodeX = 0;
        int nodeY = 0;
        switch (direction)
        {
            case "up": nodeX = 0; nodeY = 1; directionState = DirectionState.Up; break;
            case "down": nodeX = 0; nodeY = -1; directionState = DirectionState.Down; break;
            case "left": nodeX = -1; nodeY = 0; directionState = DirectionState.Left; break;
            case "right": nodeX = 1; nodeY = 0; directionState = DirectionState.Right; break;
        }
        nodeX += Player.instance.gridAgent.nodeX;
        nodeY += Player.instance.gridAgent.nodeY;
        NodeClass targetNode = Node.instance.nodeGrid.grid[nodeX, nodeY];

        if (!CheckIfWalkable(targetNode))
            return; //return if empty. shouldn't waste a player's turn
        
        bool transportDoor = CheckTransport(targetNode);

        Player.instance.gridAgent.SetNode(targetNode);

        //not sure if i'll use this tbh but i just have it for now
        Player.instance.UpdatePos(targetNode.worldPos);
        SaveSystem.Instance.SaveGame();


        Player.instance.OccludePlayer(false);
        StartCoroutine(MoveToTarget(targetNode, transportDoor));
    }
    IEnumerator MoveToTarget(NodeClass targetNode, bool transportDoor)
    {

        if (SceneSystem.instance != null
        && SceneSystem.instance.GetSceneType() == SceneState.SceneType.Combat)
            TurnManager.instance.ChangeTurn(TurnManager.State.EnemyTurn);
        else
            TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);

        Vector3 start = playerRB.position;
        Vector3 goal = targetNode.worldPos;
        Quaternion goalQuat = Quaternion.LookRotation(goal - start);
        Quaternion startQuat = playerRB.rotation;
        float t = 0;
        float t2 = 0;
        while (t < 1)
        {
            //run toggle allows smooth walk to run movement speeds/animations
            float duration = runToggle ? TurnManager.instance.runDuration : TurnManager.instance.movementDuration;
            if (runToggle)
                Player.instance.StateChange(PlayerState.State.Running);
            else
                Player.instance.StateChange(PlayerState.State.Walking);
            if (t2 < 1)
            {
                t2 += Time.fixedDeltaTime / TurnManager.instance.rotateDuration;
                playerRB.MoveRotation(Quaternion.Slerp(startQuat, goalQuat, t2));
            }
            else
                playerRB.rotation = goalQuat;
            t += Time.fixedDeltaTime / duration;
            playerRB.MovePosition(Vector3.Lerp(start, goal, t));
            yield return new WaitForFixedUpdate();
        }
        if (t >= 1)
        {
            if (transportDoor)
            {
                TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
                targetNode.transportController.InitiateTransport();
                yield break;
            }
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            playerRB.position = goal;
            playerRB.rotation = goalQuat;
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
    public void MoveBoulder(NodeClass goalNode)
    {
        Player.instance.gridAgent.SetNode(goalNode);
        StartCoroutine(TrackMoveBoulder(goalNode));
    }
    IEnumerator TrackMoveBoulder(NodeClass goalNode)
    {
        Vector3 start = playerRB.position;
        Vector3 goal = goalNode.worldPos;

        float duration = TurnManager.instance.pushDuration;
        float t = 0;
        while (t < 1)
        {
            t += Time.fixedDeltaTime / duration;
            playerRB.MovePosition(Vector3.Lerp(start, goal, t));
            yield return new WaitForFixedUpdate();
        }
        if (t >= 1)
        {
            playerRB.position = goal;
        }
    }
    public void Climb(NodeClass ladderNode)
    {
        Player.instance.StateChange(PlayerState.State.Climb);
        StartCoroutine(InitiateClimb(ladderNode));
    }
    IEnumerator InitiateClimb(NodeClass ladderNode)
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
        Vector3 start = playerRB.position;
        Vector3 goal = Player.instance.gridAgent.node.worldPos + new Vector3(0, 3, 0);
        float t = 0;
        while (t < 1)
        {
            if (t > TransitionData.LadderToSceneTime)
            {
                LoadSystem.Scene scene = ladderNode.ladderController.GetTargetScene();
                LoadSystem.instance.LoadScene(scene);
                yield break;
            }
            float duration = TurnManager.instance.climbDuration;

            t += Time.fixedDeltaTime / duration;
            playerRB.MovePosition(Vector3.Lerp(start, goal, t));
            yield return new WaitForFixedUpdate();
        }
        if (t >= 1)
        {
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            playerRB.position = goal;

            Player.instance.StateChange(PlayerState.State.Idle);
        }
    }
}