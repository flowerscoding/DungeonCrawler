using UnityEngine;

public class TutorialState : MonoBehaviour
{
    public enum State
    {
        Normal,
        Sprint,
        Destructible,
        Interact,
        Attack,
        Parry,
    }
    public State state;

    public void ChangeState(State newState)
    {
        state = newState;
    }
    public void ExectuteState()
    {
        switch (state)
        {
            case State.Sprint:
                SprintState();
                break;
            case State.Destructible:
                DestructibleState();
                break;
            case State.Parry:
                ParryState();
                break;
            case State.Normal:
                NormalState();
                break;
        }
    }
    void SprintState()
    {
        string text = "PRESS SHIFT TO SPRINT";
        TutorialManager.instance.InitiateTransition(text);
    }
    void DestructibleState()
    {
        string text = "PRESS LEFT CLICK TO CUT DOWN THE VINES (DESTRUCTIBLE)";
        TutorialManager.instance.InitiateTransition(text);
    }
    void ParryState()
    {
        string text = "PRESS SPACE BAR TO PARRY ENEMY ATTACK\nA white spark will appear, signaling when to parry";
        TutorialManager.instance.InitiateTransition(text);
    }
    void NormalState()
    {
        TutorialManager.instance.EndTransition();
    }
    public bool GetState(State checkState)
    {
        return state == checkState;
    }
}
