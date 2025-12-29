using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum State
    {
        Inactive,
        Active,
        Dead,
    }
    public State state;
}
