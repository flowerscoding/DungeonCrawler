using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Running,
        Aggro,
        Attacking,
        Hurt,
        Dead,
        Push,
        PushFail,
        Block,
    }
    public State state;
}
