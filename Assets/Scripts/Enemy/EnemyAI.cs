using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public enum Action
    {
        Move,
        Attack,
    }
    public void DecideAction(EnemyController enemyController)
    {
        Action action = ActionCheck(enemyController);
        switch(action)
        {
            case Action.Move: enemyController.enemyMovement.MoveAI(); break;
            case Action.Attack: print("ATTACK"); break;
        }
    }
    Action ActionCheck(EnemyController enemyController)
    {
        int dx = Mathf.Abs(enemyController.gridAgent.nodeX - Player.instance.playerGridAgent.nodeX); 
        int dy = Mathf.Abs(enemyController.gridAgent.nodeY - Player.instance.playerGridAgent.nodeY);
        int distance = dx + dy;
        if(distance <= 1)
            return Action.Attack;
        else
            return Action.Move;
    }
}
