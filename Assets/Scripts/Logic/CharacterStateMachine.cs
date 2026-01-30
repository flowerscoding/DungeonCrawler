using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        Idle2,
        Walking,
        Running,
        Aggro,
        Attacking,
        Hurt,
        Dead,
        Push,
        PushFail,
        Block,
        Climb,
        Destroy,
        Pray,
    }
}
