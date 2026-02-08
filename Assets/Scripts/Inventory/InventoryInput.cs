using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInput : MonoBehaviour
{
    InputAction _inventoryToggle;
    InputAction _upAction;
    InputAction _downAction;
    InputAction _leftAction;
    InputAction _rightAction;
    InputAction _leftTab;
    InputAction _rightTab;
    void Awake()
    {
        _inventoryToggle = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("InventoryToggle");
        _upAction = InputManager.instance.inputActions.asset.FindActionMap("InventoryMenu").FindAction("Up");
        _downAction = InputManager.instance.inputActions.asset.FindActionMap("InventoryMenu").FindAction("Down");
        _leftAction = InputManager.instance.inputActions.asset.FindActionMap("InventoryMenu").FindAction("Left");
        _rightAction = InputManager.instance.inputActions.asset.FindActionMap("InventoryMenu").FindAction("Right");

        _leftTab = InputManager.instance.inputActions.asset.FindActionMap("InventoryMenu").FindAction("LeftTab");
        _rightTab = InputManager.instance.inputActions.asset.FindActionMap("InventoryMenu").FindAction("RightTab");
    }
    void OnEnable()
    {
        _leftTab.performed += LeftTab;
        _rightTab.performed += RightTab;
        _inventoryToggle.performed += InventoryToggled;
        _upAction.performed += UpPressed;
        _downAction.performed += DownPressed;
        _leftAction.performed += LeftPressed;
        _rightAction.performed += RightPressed;
    }
    void OnDisable()
    {
        _leftTab.performed -= LeftTab;
        _rightTab.performed -= RightTab;
        _inventoryToggle.performed -= InventoryToggled;
        _upAction.performed -= UpPressed;
        _downAction.performed -= DownPressed;
        _leftAction.performed -= LeftPressed;
        _rightAction.performed -= RightPressed;
    }
    void InventoryToggled(InputAction.CallbackContext ctx)
    {
        Inventory.Instance.InventoryToggled();
    }
    void UpPressed(InputAction.CallbackContext ctx)
    {
        Inventory.Instance.YChange(-1);
    }
    void DownPressed(InputAction.CallbackContext ctx)
    {
        Inventory.Instance.YChange(1);
    }
    void LeftPressed(InputAction.CallbackContext ctx)
    {
        Inventory.Instance.XChange(-1);
    }
    void RightPressed(InputAction.CallbackContext ctx)
    {
        Inventory.Instance.XChange(1);
    }
    void LeftTab(InputAction.CallbackContext ctx)
    {
        Inventory.Instance.TabChange(-1);
    }
    void RightTab(InputAction.CallbackContext ctx)
    {
        Inventory.Instance.TabChange(1);
    }
}
