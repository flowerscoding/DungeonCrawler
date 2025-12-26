using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Attacking,
        Hurt,
        Dead
    }
    public State state;
}
