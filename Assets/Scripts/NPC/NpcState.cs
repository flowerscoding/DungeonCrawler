using UnityEngine;

public class NpcState : MonoBehaviour
{
    public enum State
    {
        Idle,
        Idle2,
    }
    public State state { get; private set; }

    public void StateChange(State newState, AnimateMachine animateMachine)
    {
        state = newState;
        switch (state)
        {
            case State.Idle:
                animateMachine.Animate(AnimateMachine.State.Idle);
                break;
            case State.Idle2:
                animateMachine.Animate(AnimateMachine.State.Idle2);
                break;
        }
    }
}
