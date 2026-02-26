using UnityEngine;

public class PlayerAnimationFunctions : MonoBehaviour
{
    public ParticleSystem[] landStaggerEffects;
    public void IdleSwitch()
    {
        Player.instance.StateChange(PlayerState.State.Idle);
        Player.instance.playerBlock.BlockOn();
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
    }
    public void LandStaggerEffects()
    {
        foreach (ParticleSystem effect in landStaggerEffects)
            effect.Play();
    }
    public void PlayerDied()
    {
        Player.instance.PlayerDied();
    }
}
