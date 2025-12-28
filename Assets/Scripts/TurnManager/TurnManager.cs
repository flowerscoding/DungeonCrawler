using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public enum State
    {
        PlayerTurn,
        EnemyTurn,
        Resolving,
    }
    public State state;
    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn,
        PlayerAttacking,
        EnemyAttacking,
    }
    public BattleState battleState;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void ChangeTurn(State newState)
    {
        state = newState;
    }
    public void ChangeBattleTurn(BattleState newState)
    {
        battleState = newState;
    }
}
