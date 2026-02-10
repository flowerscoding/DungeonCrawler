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
    private EnemyData.Attack _activeAttack;
    float attackDuration = 0;
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
        if (!Player.instance.playerBlock.parried)
            Player.instance.TakeDamage(_activeAttack.attackDamage);
        else
            Player.instance.playerBlock.ResetParried();

        _attackCharge = null;
        controller.CheckSurrounding();
    }
    public void EnableAttackCharge(bool enable)
    {
        if (enable && _attackCharge == null)
        {
            print("ENABLE!");
            attackDuration = 0;
            chargeBar.enabled = true;
            DetermineAttack();
            _attackCharge = StartCoroutine(AttackCharge());
        }
        else if (!enable && _attackCharge != null)
        {
            print("DISABLE!");
            StopCoroutine(_attackCharge);
            _attackCharge = null;
        }
    }
    IEnumerator AttackCharge()
    {
        while (attackDuration < 1)
        {

            chargeBar.fillAmount = attackDuration;
            attackDuration += Time.deltaTime / _activeAttack.chargeDuration;
            yield return null;
        }
        print("FULL");
        while (TurnManager.instance.state != TurnManager.State.PlayerTurn)
        {
            print(TurnManager.instance.state);
            yield return null;
        }
            StartCoroutine(Attack());
    }
    IEnumerator Attack()
    {
        if (controller.enemyState.state != EnemyState.State.Active) yield break;
        while (TurnManager.instance.state != TurnManager.State.PlayerTurn)
        {
            yield return null;
        }
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);

        controller.NewState(EnemyAI.State.Attacking);
        print("ENEMY ATTACK");
    }
    void DetermineAttack()
    {
        _activeAttack = enemyData.attacks[Random.Range(0, enemyData.attacks.Length)];
        print("CHOSEN ATTACK: " + _activeAttack.attackName);
    }
}
