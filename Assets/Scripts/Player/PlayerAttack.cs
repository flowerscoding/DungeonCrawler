using System.Collections;
using UnityEngine.UI;
using UnityEngine;


public class PlayerAttack : MonoBehaviour
{
    public NodeClass targetNode; //for PlayerState script
    public int damageOutput;
    public float attackChargeMax;
    bool _chargeEnabled;
    [SerializeField] Canvas _chargeCanvas;
    [SerializeField] Image _chargeBar;
    bool _attackReady;
    float _attackChargeAmount = 0;
    public void EnableAttackCharge(bool enableUI, bool runCharge)
    {
        _chargeCanvas.enabled = enableUI;
        _chargeEnabled = runCharge;
        if (runCharge)
            StartCoroutine(AttackCharge());
        else
        {
            StopCoroutine(AttackCharge());
            _attackReady = false;
        }
    }
    IEnumerator AttackCharge()
    {
        while (_attackChargeAmount < 1)
        {
            if (!_chargeEnabled)
                yield break;

            _attackChargeAmount += Time.deltaTime / attackChargeMax;
            _chargeBar.fillAmount = _attackChargeAmount;
            yield return null;
        }
        if (_attackChargeAmount >= 1)
            _attackReady = true;
    }
    public void AttackPressed()
    {
        targetNode = NodeFacing();

        if (TurnManager.instance.state == TurnManager.State.PlayerTurn
        && targetNode.enemyController != null
        && targetNode.enemyController.enemyState.state == EnemyState.State.Active
        && targetNode.enemyController.enemyHealth.curHealth > 0f
        && _attackReady)
            ExecuteAttack();
    }
    void ExecuteAttack()
    {
        damageOutput = AttackDamageCalculation();
        Player.instance.StateChange(PlayerState.State.Attacking);
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerAttacking);
        Enemy.instance.PauseCharges(true);

        StopCoroutine(AttackCharge());
        _chargeCanvas.enabled = false;
        _chargeBar.fillAmount = 0;
        _attackChargeAmount = 0;
        _attackReady = false;
    }
    public void AttackLanded()
    {
        if (targetNode.state == NodeClass.State.Enemy)
            targetNode.enemyController.TakeDamage(damageOutput);
        Enemy.instance.PauseCharges(false);
        bool enable = Player.instance.CheckSurrounding(PlayerSurrounding.CheckType.ActiveEnemy);
        EnableAttackCharge(enable, enable);
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
