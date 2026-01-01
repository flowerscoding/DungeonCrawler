using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class ActionsMenuState : MonoBehaviour
{
    public Image labelHighlight;
    public Vector3 labelPos;
    public enum State
    {
        Attack,
        Parry,
        Exit,
    }
    public State state {get; private set;}
    public void ResetState()
    {
        state = 0;
        labelPos = new Vector3(0, 70, 0);
        labelHighlight.rectTransform.anchoredPosition = labelPos;
    }
    public void StateAction()
    {
        switch(state)
        {
            case State.Attack: AttackAction(); break;
            case State.Parry: ParryAction(); break;
            case State.Exit: ExitAction(); break;
        }
    }
    void AttackAction()
    {
        Player.instance.playerAttack.AttackPressed();
        ActionsMenu.instance.DisableActionsMenu();
    }
    void ParryAction()
    {
        ActionsMenu.instance.DisableActionsMenu();
    }
    void ExitAction()
    {
        ActionsMenu.instance.DisableActionsMenu();
    }
    public void ChangeState(bool up)
    {
        int addition = up ? -1 : 1;
        addition += (int)state;
        if(addition < 0 || addition > 2) return;

        state = (State)addition;
        switch(state)
        {
            case State.Attack: 
                labelPos = new Vector3(0, 70, 0);
            break;
            case State.Parry: 
                labelPos = new Vector3(0, 0, 0);
            break;
            case State.Exit: 
                labelPos = new Vector3(0, -70, 0);
            break;
        }
        labelHighlight.rectTransform.anchoredPosition = labelPos;
    }
}
