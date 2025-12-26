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
        Dead
    }
    public State state;
}
