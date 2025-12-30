using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum State
    {
        Inactive,
        Active,
        Dead,
    }
    public State state {get; private set;}
    public void NewState(State newState)
    {
        state = newState;
    }
}
