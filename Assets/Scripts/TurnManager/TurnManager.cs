using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public enum State
    {
        PlayerTurn,
        EnemyTurn,
    }
    public State state;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void EndPlayerTurn()
    {
        state = State.EnemyTurn;
    }
    public void StartPlayerTurn()
    {
        state = State.PlayerTurn;
    }
}
