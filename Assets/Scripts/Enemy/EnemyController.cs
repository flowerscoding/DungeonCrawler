using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    public GridAgent gridAgent;
    public EnemyData enemyData;
    public EnemyAI enemyAI;
    public EnemyMovement enemyMovement;
    public EnemyState enemyState;
    public AnimateMachine animateMachine;
    public CharacterStateMachine characterStateMachine;
    public EnemyHealth enemyHealth;
    public EnemyAttack enemyAttack;
    public Vector3 deadSpot;
    public void MoveCharacter()
    {
        Action<CharacterStateMachine.State> characterStateFunction = CharacterStateFunction;
        enemyMovement.MoveAI(characterStateFunction);
    }
    public void CharacterStateFunction(CharacterStateMachine.State newState)
    {
        characterStateMachine.state = newState;
        if (newState != CharacterStateMachine.State.Dead)
            animateMachine.Animate(newState);
        switch (newState)
        {
            case CharacterStateMachine.State.Walking: MoveCharacter(); break;
            case CharacterStateMachine.State.Attacking: animateMachine.Animate(CharacterStateMachine.State.Attacking); break;
            case CharacterStateMachine.State.Hurt: StartCoroutine(TrackHurt(false)); break;
            case CharacterStateMachine.State.Dead:
                Enemy.instance.enemySystem.activeEnemies.Remove(this);
                enemyState.state = EnemyState.State.Dead;
                animateMachine.Animate(CharacterStateMachine.State.Hurt);
                StartCoroutine(TrackHurt(true));
                break;
            default: break;
        }
    }
    IEnumerator TrackHurt(bool dead)
    {
        yield return null; //wait a frame to help reset the animator
        AnimatorStateInfo info = animateMachine.animator.GetCurrentAnimatorStateInfo(0);
        float progress = info.normalizedTime;
        while (progress < 1)
        {
            info = animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if (progress >= 1)
        {
            if (dead)
            {
                ExecuteDeath();
                TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
                TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.PlayerTurn);
            }
            else
            {
                AttackPlayer();
            }
        }
    }
    public void DecideAction()
    {
        CharacterStateFunction(enemyAI.DecideAction(gridAgent));
    }
    public void AttackPlayer()
    {
        enemyAttack.AttackPlayer(this);
    }
    public void TakeDamage(int damage)
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.EnemyTurn);
        TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.EnemyTurn);
        enemyHealth.curHealth -= damage;
        if (enemyHealth.curHealth <= 0)
        {
            CharacterStateFunction(CharacterStateMachine.State.Dead);
        }
        else
            CharacterStateFunction(CharacterStateMachine.State.Hurt);
    }
    public void ExecuteDeath()
    {
        Enemy.instance.enemySystem.activeEnemies.Remove(this);
        enemyState.state = EnemyState.State.Dead;
        StartCoroutine(RunDeathAnimation());
    }
    IEnumerator RunDeathAnimation()
    {
        yield return null; //wait a frame to help reset the animator
        AnimatorStateInfo info = animateMachine.animator.GetCurrentAnimatorStateInfo(0);
        float progress = info.normalizedTime;
        animateMachine.Animate(CharacterStateMachine.State.Dead);
        while (progress < 1)
        {
            info = animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if (progress >= 1)
        {
            transform.position = deadSpot;
            Node.instance.ResetNode(gridAgent.node);
        }
    }
}
