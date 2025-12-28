using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private InputAction _attackButton;
    void Awake()
    {
        _attackButton = InputManager.instance.inputActions.asset.FindActionMap("Player").FindAction("Attack");
    }
    void OnEnable()
    {
        _attackButton.performed += AttackPressed;
    }
    void AttackPressed(InputAction.CallbackContext ctx)
    {
        if (TurnManager.instance.state != TurnManager.State.PlayerTurn
        || TurnManager.instance.battleState != TurnManager.BattleState.PlayerTurn)
            return;
        NodeClass targetNode = NodeFacing();

        if (targetNode.occupant != null)
            ExecuteAttack(targetNode);
    }
    void ExecuteAttack(NodeClass targetNode)
    {
        //TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.PlayerAttacking);
        targetNode.occupant.TakeDamage(AttackDamageCalculation());
        print(targetNode.occupant.enemyHealth.curHealth);
    }
    int AttackDamageCalculation()
    {//ADD OR COMPONENTS IN THE FUTURE FOR DAMAGE FACTORS
        int attackDamage = Player.instance.playerData.attackDamage;
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
