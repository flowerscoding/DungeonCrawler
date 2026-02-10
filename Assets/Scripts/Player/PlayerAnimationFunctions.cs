using UnityEngine;

public class PlayerAnimationFunctions : MonoBehaviour
{
    public void IdleSwitch()
    {
        Player.instance.StateChange(PlayerState.State.Idle);
        Player.instance.playerBlock.BlockOn();
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
    }
    public void AttackEnded()
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
    }
}
