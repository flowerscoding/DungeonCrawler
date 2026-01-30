using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager instance;
    public float movementDuration;
    public float rotateDuration;
    public float runDuration;
    public float pushDuration;
    public float climbDuration;
    public enum State
    {
        PlayerTurn,
        EnemyTurn,
        Resolving,
        Dialogue,
    }
    public State state;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public void ChangeTurn(State newState)
    {
        state = newState;
        switch (newState)
        {
            case State.EnemyTurn: Enemy.instance.enemySystem.MoveAI(); break;
            case State.PlayerTurn: Player.instance.playerBlock.BlockOn(); break;
            case State.Resolving: break;
        }
    }
}
