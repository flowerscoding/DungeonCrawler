using UnityEngine;

public class ActionsMenu : MonoBehaviour
{
    public static ActionsMenu instance;
    public Canvas actionsMenuUI;
    public ActionsMenuState actionsMenuState;
    public ActionsMenuInput actionsMenuInput;
    void Awake()
    {
        if(instance == null)
            instance = this;
    }
    public void EnableActionsMenu()
    {
        actionsMenuState.ResetState();
        actionsMenuUI.enabled = true;
        InputManager.instance.MapChange(InputMapping.MapType.ActionsMenu);
    }
    public void DisableActionsMenu()
    {
        actionsMenuUI.enabled = false;
        InputManager.instance.MapChange(InputMapping.MapType.Player);
    }
    public void ConfirmedAction()
    {
        actionsMenuState.StateAction();
    }
    public void UpAction()
    {
        actionsMenuState.ChangeState(true);
    }
    public void DownAction()
    {
        actionsMenuState.ChangeState(false);
    }
}
