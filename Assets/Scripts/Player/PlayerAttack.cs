using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public NodeClass targetNode; //for PlayerState script
    public int damageOutput;
    bool _chargeEnabled;
    [SerializeField] Canvas _chargeCanvas;
    [SerializeField] Image _chargeBar;
    float _attackChargeAmount = 0;
    Coroutine _runningCharge;
    public void EnableAttackCharge(bool enableUI, bool runCharge)
    {
        _chargeCanvas.enabled = enableUI;
        _chargeEnabled = runCharge;
        if (runCharge && _runningCharge == null)
            _runningCharge = StartCoroutine(AttackCharge());
        else if (_runningCharge != null)
        {
            StopCoroutine(_runningCharge);
            _runningCharge = null;
        }
    }
    IEnumerator AttackCharge()
    {
        while (_attackChargeAmount < 1)
        {
            if (!_chargeEnabled)
            {
                StopCoroutine(_runningCharge);
                yield break;
            }

            _attackChargeAmount += Time.deltaTime / 5;
            _chargeBar.fillAmount = _attackChargeAmount;
            yield return null;
        }
    }
    public void AttackPressed()
    {
        targetNode = NodeFacing();

        if (TurnManager.instance.state == TurnManager.State.PlayerTurn
        && targetNode.enemyController != null
        && targetNode.enemyController.enemyState.state == EnemyState.State.Active)
        {
            if (targetNode.enemyController.enemyHealth.curHealth > 0f)
            {
                float enemyRotDuration = 0.2f;
                targetNode.enemyController.RotateEnemyBody(Player.instance.playerMovement.playerRB.position, enemyRotDuration);
                if (targetNode.enemyController.enemyHealth.curStagger >= targetNode.enemyController.enemyData.maxStagger)
                    ExecuteStaggerAttack();
                else
                    ExecuteAttack();
            }
        }
    }
    void ExecuteAttack()
    {
        damageOutput = AttackDamageCalculation();
        Player.instance.StateChange(PlayerState.State.Attacking);
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerAttacking);
        //Enemy.instance.PauseCharges(true);

        if (_runningCharge != null)
        {
            StopCoroutine(_runningCharge);
            _runningCharge = null;
        }
        _chargeCanvas.enabled = false;
        _chargeBar.fillAmount = 0;
        _attackChargeAmount = 0;
    }
    void ExecuteStaggerAttack()
    {
        Player.instance.StateChange(PlayerState.State.StaggerAttacking);
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerAttacking);
        //Enemy.instance.PauseCharges(true);

        if (_runningCharge != null)
        {
            StopCoroutine(_runningCharge);
            _runningCharge = null;
        }
        _chargeCanvas.enabled = false;
        _chargeBar.fillAmount = 0;
        _attackChargeAmount = 0;
    }
    public void AttackLanded()
    {
        if (targetNode.state == NodeClass.State.Enemy)
            targetNode.enemyController.TakeDamage(damageOutput);
    }
    public void EndTurn()
    {
        //Enemy.instance.PauseCharges(false);
        bool enable = Player.instance.CheckSurrounding(PlayerSurrounding.CheckType.ActiveEnemy);
        EnableAttackCharge(enable, enable);
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
    }
    public void StaggerAttackLanded()
    {
        if (targetNode.state == NodeClass.State.Enemy)
            targetNode.enemyController.StaggerAttackReceived();
    }
    int AttackDamageCalculation()
    {//Damage output factors can be added
        int attackDamage =
        Player.instance.playerData.attackStat *
        Player.instance.playerData.weapon.data.damage;
        float weakDamage = 0.3f;
        float normalDamage = 0.5f;
        float strongDamage = 0.8f;
        if (_attackChargeAmount < 0.5f)
            attackDamage = (int)(attackDamage * weakDamage);
        else if (_attackChargeAmount < 0.8f)
            attackDamage = (int)(attackDamage * normalDamage);
        else if (_attackChargeAmount < 1f)
            attackDamage = (int)(attackDamage * strongDamage);
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
