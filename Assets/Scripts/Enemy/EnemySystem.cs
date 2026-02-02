using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public Transform enemiesParent;
    List<EnemyController> _enemies = new List<EnemyController>();
    public List<EnemyController> activeEnemies;
    public int activateDistance;
    void Awake()
    {
        foreach(Transform enemy in enemiesParent)
            _enemies.Add(enemy.GetComponent<EnemyController>());
    }
    public void MoveAI()
    { //kind of pointless since this only gets called by the players move decision but its defensive programming i guess lol
        if(TurnManager.instance.state != TurnManager.State.EnemyTurn)
            return;
            
        ActivateEnemies();
    }
    void ActivateEnemies()
    {
        activeEnemies.Clear();
        foreach(EnemyController enemy in _enemies)
        {
            int xDis = Mathf.Abs(enemy.gridAgent.nodeX - Player.instance.gridAgent.nodeX);
            int yDis = Mathf.Abs(enemy.gridAgent.nodeY - Player.instance.gridAgent.nodeY);
            if(xDis + yDis < activateDistance && enemy.enemyState.state != EnemyState.State.Dead)
            {
                enemy.enemyState.NewState(EnemyState.State.Active);
                activeEnemies.Add(enemy);
            }
            else if (enemy.enemyState.state != EnemyState.State.Dead)
                enemy.enemyState.NewState(EnemyState.State.Inactive);
            else
            {
                enemy.enemyState.NewState(EnemyState.State.Dead);
            }
        }
        DecideActions();
    }
    void DecideActions()
    {
        foreach(EnemyController enemy in activeEnemies)
        {
            if(enemy.enemyState.state == EnemyState.State.Active)
            {
                enemy.NewState(EnemyAI.State.Walking);
            }
        }
    }
    public void ActivateEnemy(EnemyController controller)
    {
        activeEnemies.Add(controller);
    }
}