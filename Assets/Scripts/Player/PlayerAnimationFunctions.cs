using UnityEngine;

public class PlayerAnimationFunctions : MonoBehaviour
{
    public ParticleSystem[] landStaggerEffects;
    public void EndTurn() //attack animations
    {
        Player.instance.StateChange(PlayerState.State.Idle);
        Player.instance.playerBlock.BlockOn();
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
    }
    public void LandStaggerEffects() //staggerAttack animation
    {
        foreach (ParticleSystem effect in landStaggerEffects)
            effect.Play();
    }
    public void PlayerDied() //dead animation
    {
        Player.instance.PlayerDied();
    }
    public void EndEnemyTurn() //hurt animation
    {
        Player.instance.playerBlock.BlockOn();
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        Enemy.instance.PauseCharges(false);
        Player.instance.EnableAttackCharge(true, true);
    }
}
