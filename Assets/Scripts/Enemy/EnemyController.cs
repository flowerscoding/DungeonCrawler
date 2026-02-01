using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GridAgent gridAgent;
    public EnemyData enemyData;
    public EnemyAI enemyAI;
    public EnemyMovement enemyMovement;
    public EnemyState enemyState;
    public AnimateMachine animateMachine;
    public EnemyHealth enemyHealth;
    public EnemyAttack enemyAttack;
    public float stagger;
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
    public void TakeStagger(float amount)
    {
        stagger += amount;
        if(stagger > enemyData.staggerLimit)
        {
            print("OVER STAGGER!    stagger: " + stagger);
        }
        else
            print("CurStagger = " + stagger);
    }
    public void ResetEnemy()
    {
        enemyState.NewState(EnemyState.State.Active);
        enemyAI.NewState(EnemyAI.State.Idle, this);
        Enemy.instance.ActivateEnemy(this);
        SetNode();
        enemyHealth.curHealth = enemyData.maxHealth;
    }
    public void SetNode()
    {
        int x = Mathf.FloorToInt(transform.position.x - Node.instance.nodeGrid.gridOrigin.x);
        int y = Mathf.FloorToInt(transform.position.z - Node.instance.nodeGrid.gridOrigin.z);

        gridAgent.SetNode(Node.instance.nodeGrid.grid[x, y]);
    }
    public void OpenParry(bool open)
    {
        enemyAttack.parryable = open;
    }
    public bool ParryableCheck()
    {
        return enemyAttack.parryable;
    }
}
