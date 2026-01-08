using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInputActions inputActions;

    public InputMapping inputMapping;
    public InputController inputControllerState;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        inputActions = new PlayerInputActions();
        inputActions.Disable();
        inputActions.asset.actionMaps[0].Enable();
    }
    public void MapChange(InputMapping.MapType newMap)
    {
        inputMapping.MapChange(newMap);
    }
    public void ControllerTypeCheck(InputAction.CallbackContext ctx)
    {
        InputController.Type newType;
        switch (ctx.control.device)
        {
            case Mouse:
                newType = InputController.Type.Keyboard;
                break;
            case Keyboard:
                newType = InputController.Type.Keyboard;
                break;
            case Gamepad:
                newType = InputController.Type.Gamepad;
                break;
            default:
                newType = InputController.Type.Keyboard;
                break;
        }
        ControllerChange(newType);
    }
    void ControllerChange(InputController.Type newType)
    {
        inputControllerState.ChangeController(newType);
    }
}
