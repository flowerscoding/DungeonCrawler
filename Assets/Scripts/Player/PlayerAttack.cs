using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private InputAction _attackButton;
    public NodeClass targetNode; //for PlayerState script
    public int damageOutput;
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
        targetNode = NodeFacing();

        if (TurnManager.instance.state == TurnManager.State.PlayerTurn
        && targetNode.occupant != null 
        && targetNode.occupant.enemyState.state  == EnemyState.State.Active
        && targetNode.occupant.enemyHealth.curHealth > 0f)
            ExecuteAttack();
        else if(TurnManager.instance.state == TurnManager.State.PlayerTurn)
            ExecuteEmptyAttack();
    }
    void ExecuteAttack()
    {
        damageOutput = AttackDamageCalculation();
        Player.instance.StateChange(PlayerState.State.Attacking);
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
    }
    void ExecuteEmptyAttack()
    {
        Player.instance.StateChange(PlayerState.State.Attacking);
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
    }
    int AttackDamageCalculation()
    {//ADD MORE COMPONENTS IN THE FUTURE FOR DAMAGE FACTORS
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
