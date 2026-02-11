using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public NodeClass targetNode; //for PlayerState script
    public int damageOutput;
    public void AttackPressed()
    {
        targetNode = NodeFacing();

        if (TurnManager.instance.state == TurnManager.State.PlayerTurn
        && targetNode.enemyController != null 
        && targetNode.enemyController.enemyState.state  == EnemyState.State.Active
        && targetNode.enemyController.enemyHealth.curHealth > 0f)
            ExecuteAttack();
        else if(TurnManager.instance.state == TurnManager.State.PlayerTurn)
            ExecuteEmptyAttack();
    }
    void ExecuteAttack()
    {
        damageOutput = AttackDamageCalculation();
        Player.instance.StateChange(PlayerState.State.Attacking);
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerAttacking);
    }
    public void AttackLanded()
    {
        if(targetNode.state == NodeClass.State.Enemy)
            targetNode.enemyController.TakeDamage(damageOutput);
    }
    void ExecuteEmptyAttack()
    {
        Player.instance.StateChange(PlayerState.State.Attacking);
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerAttacking);
    }
    int AttackDamageCalculation()
    {//Damage output factors can be added

        int attackDamage = 
        Player.instance.playerData.attackStat *
        Player.instance.playerData.weapon.data.damage;
        return attackDamage;
    }
    NodeClass NodeFacing()
    {
        NodeClass checkNode;
        int x = 0;
        int y = 0;
        Transform playerT = Player.instance.playerMovement.playerRB.transform;
        Vector3 facingDirection = playerT.forward;
        Vector3 absDirection = new Vector3(Mathf.Abs(facingDirection.x), 0, Mathf.Abs(facingDirection.z));
        if (absDirection.x > absDirection.z)
        {
            if (facingDirection.x > 0)
            {
                x = Player.instance.gridAgent.nodeX + 1;
                y = Player.instance.gridAgent.nodeY;
            }
            else
            {
                x = Player.instance.gridAgent.nodeX - 1;
                y = Player.instance.gridAgent.nodeY;
            }
        }
        else
        {
            if (facingDirection.z > 0)
            {
                x = Player.instance.gridAgent.nodeX;
                y = Player.instance.gridAgent.nodeY + 1;
            }
            else
            {
                x = Player.instance.gridAgent.nodeX;
                y = Player.instance.gridAgent.nodeY - 1;
            }
        }
        checkNode = Node.instance.nodeGrid.grid[x, y];
        return checkNode;
    }
}
