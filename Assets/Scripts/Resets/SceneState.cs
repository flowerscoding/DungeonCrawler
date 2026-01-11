using UnityEngine;

public class SceneState : MonoBehaviour
{
    
    public enum State
    {
        Normal,
        Reset,
    }
    public State state {get; private set;}
    
    public void StateChange(State newState)
    {
        state = newState;
    }
    public void RunState()
    {
        switch(state)
        {
            case State.Normal: break;
            case State.Reset: SceneSystem.instance.RunReset(); break;
        }
    }
}
