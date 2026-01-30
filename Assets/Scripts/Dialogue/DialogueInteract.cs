using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueInteract : MonoBehaviour
{
    public InputAction _confirmAction;
    public bool active;
    void Awake()
    {
        _confirmAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Interact");
    }
    void OnEnable()
    {
        _confirmAction.performed += NextLine;
    }
    void OnDisable()
    {
        _confirmAction.performed -= NextLine;
    }
    void NextLine(InputAction.CallbackContext ctx)
    {
        if(TurnManager.instance.state != TurnManager.State.Dialogue) return;
        if(!active)
        {
            active = true;
            return;
        }
        DialogueSystem.instance.RunDialogue();
    }
}
