using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public enum Action
    {
        Move,
        Attack,
    }
    public CharacterStateMachine.State DecideAction(GridAgent gridAgent)
    {
        CharacterStateMachine.State action = ActionCheck(gridAgent);
        return action;
    }
    CharacterStateMachine.State ActionCheck(GridAgent gridAgent)
    {
        int dx = Mathf.Abs(gridAgent.nodeX - Player.instance.gridAgent.nodeX); 
        int dy = Mathf.Abs(gridAgent.nodeY - Player.instance.gridAgent.nodeY);
        int distance = dx + dy;
        if(distance <= 1)
            return CharacterStateMachine.State.Attacking;
        else
            return CharacterStateMachine.State.Walking;
    }
}
