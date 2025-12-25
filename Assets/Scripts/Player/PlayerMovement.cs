using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
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
    }
    public void MoveUp(InputAction.CallbackContext ctx)
    {
        
    }
}
