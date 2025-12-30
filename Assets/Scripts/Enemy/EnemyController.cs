using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GridAgent gridAgent;
    public EnemyData enemyData;
    public EnemyAI enemyAI;
    public EnemyMovement enemyMovement;
    public EnemyState enemyState;
    public AnimateMachine animateMachine;
    public CharacterStateMachine characterStateMachine;
    public EnemyHealth enemyHealth;
    public EnemyAttack enemyAttack;
    public Vector3 deadSpot;
    public void NewState(EnemyAI.State newState)
    {
        enemyAI.NewState(newState, this);
    }
    public void TakeDamage(int damage)
    {
        enemyHealth.curHealth -= damage;
        if(enemyHealth.curHealth > 0)
            NewState(EnemyAI.State.Hurt);
        else if (enemyHealth.curHealth <= 0)
        {
            NewState(EnemyAI.State.Dead);
        }
    }
}
