using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public EnemyController[] enemies;
    public List<EnemyController> activeEnemies;
    public int activateDistance;
    public void MoveAI()
    { //kind of pointless since this only gets called by the players move decision but its defensive programming i guess lol
        if(TurnManager.instance.state != TurnManager.State.EnemyTurn) return; 
        ActivateEnemies();
    }
    void ActivateEnemies()
    {
        activeEnemies.Clear();
        foreach(EnemyController enemy in enemies)
        {
            int xDis = Mathf.Abs(enemy.gridAgent.nodeX - Player.instance.gridAgent.nodeX);
            int yDis = Mathf.Abs(enemy.gridAgent.nodeY - Player.instance.gridAgent.nodeY);
            if(xDis + yDis < activateDistance)
            {
                enemy.enemyState.state = EnemyState.State.active;
                activeEnemies.Add(enemy);
            }
            else
                enemy.enemyState.state = EnemyState.State.inactive;
        }
        DecideActions();
    }
    void DecideActions()
    {
        foreach(EnemyController enemy in activeEnemies)
        {
            if(enemy.enemyState.state == EnemyState.State.active)
            {
                enemy.DecideAction();
            }
        }
    }
}