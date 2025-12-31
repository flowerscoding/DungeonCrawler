using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Aggro,
        Attacking,
        Hurt,
        Dead,
        Push,
        PushFail,
    }
    public State state;
}
