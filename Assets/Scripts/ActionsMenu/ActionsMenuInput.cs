using UnityEngine;
using UnityEngine.InputSystem;

public class ActionsMenuInput : MonoBehaviour
{
    private PlayerInputActions _inputActions;
    private InputAction _confirmAction;
    private InputAction _upAction;
    private InputAction _downAction;
    void Awake()
    {
        _inputActions = InputManager.instance.inputActions;
        _confirmAction = _inputActions.asset.FindActionMap("ActionsMenu").FindAction("Confirm");
        _upAction = _inputActions.asset.FindActionMap("ActionsMenu").FindAction("Up");
        _downAction = _inputActions.asset.FindActionMap("ActionsMenu").FindAction("Down");
    }
    void OnEnable()
    {
        _confirmAction.performed += ConfirmPressed;
        _upAction.performed += UpPressed;
        _downAction.performed += DownPressed;
    }
    void ConfirmPressed(InputAction.CallbackContext ctx)
    {
        ActionsMenu.instance.ConfirmedAction();
    }
    void UpPressed(InputAction.CallbackContext ctx)
    {
        ActionsMenu.instance.UpAction();
    }
    void DownPressed(InputAction.CallbackContext ctx)
    {
        ActionsMenu.instance.DownAction();
    }
}
