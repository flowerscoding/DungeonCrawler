using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyData enemyData;

    public void AttackPlayer(EnemyController enemyController)
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
        TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.EnemyAttacking);
        enemyController.CharacterStateFunction(CharacterStateMachine.State.Attacking);

        CheckDirection(enemyController);

        StartCoroutine(RunAttackAnimation(enemyController));
    }
    IEnumerator RunAttackAnimation(EnemyController enemyController)
    {
        yield return null; //wait a frame to help reset the animator
        AnimatorStateInfo info = enemyController.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
        float progress = 0;
        while (progress < 1)
        {
            info = enemyController.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            if (progress > enemyData.attackHitFrame)
            {
                //***PLAY PLAYER HURT ANIMATION / TAKE DAMAGE FUNCTION
                Player.instance.TakeDamage(enemyData.attackDamage);
            }

            yield return null;
        }
        if (progress >= 1)
        {
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.PlayerTurn);
        }
    }
    void CheckDirection(EnemyController enemyController)
    {
        StartCoroutine(TurnAnimation());
    }
    IEnumerator TurnAnimation()
    {
        Transform playerTransform = Player.instance.gridAgent.transform;
        float t = 0;
        float duration = 0.5f;
        Vector3 start = transform.position;
        Vector3 direction = playerTransform.position - start;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        while (t < duration)
        {
            t += Time.deltaTime / duration;

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, t);

            yield return null;
        }
        if(t > duration)
        {
            transform.rotation = lookRotation;
        }
    }
}
