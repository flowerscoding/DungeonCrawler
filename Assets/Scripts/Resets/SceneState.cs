using UnityEngine;

public class SceneState : MonoBehaviour
{
    
    public enum State
    {
        Normal,
        Reset,
    }
    public enum SceneType
    {
        Combat,
        Noncombat,
    }
    public State state {get; private set;}
    [SerializeField] SceneType _sceneType;
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
    public SceneType GetSceneType()
    {
        return _sceneType;
    }
}
