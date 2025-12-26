using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum State
    {
        inactive,
        active,
    }
    public State state;
}
