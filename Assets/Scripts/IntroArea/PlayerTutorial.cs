using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTutorial : MonoBehaviour
{
    private InputAction _sprintAction;

    private PlayerInteract _playerInteract;

    void Awake()
    {
        _playerInteract = Player.instance.playerInteract;
        _playerInteract.enabled = false;
        _sprintAction = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("RunToggle");
    }
    void OnEnable()
    {
        _sprintAction.performed += Sprinted;
    }
    void Sprinted(InputAction.CallbackContext ctx)
    {
        if(!TutorialManager.instance.CheckBool(TutorialChecklist.ConfirmedBool.Sprinted))
        {
            if(TutorialManager.instance.CheckState(TutorialState.State.Sprint))
            {
                TutorialManager.instance.EndPhase();
                TutorialManager.instance.ChecklistConfirmed(TutorialChecklist.ConfirmedBool.Sprinted);

                _playerInteract.enabled = true;

                TutorialManager.instance.InitiatePhase(TutorialState.State.Vines);
            }
        }
    }
}
