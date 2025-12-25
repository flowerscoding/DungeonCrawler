using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float duration;
    public Rigidbody playerRB;

    private InputAction _upAction;
    private InputAction _downAction;
    private InputAction _leftAction;
    private InputAction _rightAction;
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
    }
    void MoveUp(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            MoveDirection("up");
    }
    void MoveDown(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            MoveDirection("down");
    }
    void MoveLeft(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            MoveDirection("left");
    }
    void MoveRight(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
            MoveDirection("right");
    }
    void MoveDirection(string direction)
    {
        if(TurnManager.instance.state != TurnManager.State.PlayerTurn) return;
        int nodeX = 0;
        int nodeY = 0;
        switch(direction)
        {
            case "up": nodeX = 0; nodeY = 1; break;
            case "down": nodeX = 0; nodeY = -1; break;
            case "left": nodeX = -1; nodeY = 0; break;
            case "right": nodeX = 1; nodeY = 0; break;
        }
        nodeX += Mathf.FloorToInt(playerRB.position.x - Node.instance.nodeGrid.gridOrigin.x); //change to + if grid origin.x is positive. same is said for z
        nodeY += Mathf.FloorToInt(playerRB.position.z - Node.instance.nodeGrid.gridOrigin.z);
        NodeClass targetNode = Node.instance.nodeGrid.grid[nodeX, nodeY];
        StartCoroutine(MoveToTarget(targetNode));
    }
    IEnumerator MoveToTarget(NodeClass targetNode)
    {

        Vector3 start = playerRB.position;
        Vector3 goal = targetNode.worldPos;
        float t = 0;
        while (t < 1)
        {
            print(2);
            t += Time.deltaTime / duration;
            playerRB.MovePosition(Vector3.Lerp(start, goal, t));
            yield return null;
        }
        if(t > 1)
            playerRB.position = goal;
    }
}
