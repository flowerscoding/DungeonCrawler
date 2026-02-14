using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyData enemyData;
    public bool parryable;
    public ParticleSystem[] loadUpParticles;
    public Light[] flashes;
    public EnemyController controller;
    Coroutine _attackCharge;
    float _attackChargeAmount = 0;
    private EnemyData.Attack _activeAttack;
    public Image chargeBar;
    public void LoadUp()
    {
        foreach (ParticleSystem ps in loadUpParticles)
            ps.Play();
    }
    public void ParryOpen()
    {
        foreach (ParticleSystem ps in loadUpParticles)
            ps.Stop();
        foreach (Light flash in flashes)
            flash.enabled = true;
        controller.OpenParry(true);
    }
    public void ParryClose()
    {
        controller.OpenParry(false);
        foreach (Light flash in flashes)
            flash.enabled = false;
        CheckHit();
    }
    void CheckHit()
    {
        Player.instance.OccludePlayer(false);
        if (!Player.instance.playerBlock.parried)
            Player.instance.TakeDamage(_activeAttack.attackDamage);
        else
            Player.instance.playerBlock.ResetParried();

        _attackCharge = null;
        controller.CheckSurrounding();

        Player.instance.SuspendMovement(false);
        Player.instance.EnableAttackCharge(true, true);
    }
    public void EnableAttackCharge(bool enable)
    {
        if (enable && _attackCharge == null)
        {
            chargeBar.enabled = true;
            DetermineAttack();
            _attackCharge = StartCoroutine(AttackCharge());
        }
        else if (!enable && _attackCharge != null)
        {
            StopCoroutine(_attackCharge);
            _attackCharge = null;
        }
    }
    public bool pauseCharges = false;
    public void PauseCharges(bool pause)
    {
        pauseCharges = pause;
    }
    IEnumerator AttackCharge()
    {
        while (_attackChargeAmount < 1)
        {
            chargeBar.fillAmount = _attackChargeAmount;
            _attackChargeAmount += pauseCharges ? 0 : Time.deltaTime / _activeAttack.chargeDuration;
            yield return null;
        }
        StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        if (controller.enemyState.state != EnemyState.State.Active) yield break;
        if (controller.gridAgent.nodeY > Player.instance.gridAgent.nodeY)
            Player.instance.OccludePlayer(true);
        Player.instance.SuspendMovement(true);
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);

        switch (_activeAttack.attackName)
        {
            case "LightAttack":
                controller.animateMachine.Animate(AnimateMachine.State.LightAttack);
                break;
            case "NormalAttack":
                controller.animateMachine.Animate(AnimateMachine.State.NormalAttack);
                break;
            case "HeavyAttack":
                controller.animateMachine.Animate(AnimateMachine.State.HeavyAttack);
                break;
        }
        controller.NewState(EnemyAI.State.Attacking);
        _attackChargeAmount = 0;

        Player.instance.EnableAttackCharge(true, false);
    }
    void DetermineAttack()
    {
        _activeAttack = enemyData.attacks[Random.Range(0, enemyData.attacks.Length)];
    }
}
