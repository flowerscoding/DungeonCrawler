using System;
using System.Collections;
using UnityEngine;

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
    public float deathAnimationDuration;
    public void MoveCharacter()
    {
        Action<CharacterStateMachine.State> characterStateFunction = CharacterStateFunction;
        enemyMovement.MoveAI(characterStateFunction);
    }
    public void CharacterStateFunction(CharacterStateMachine.State newState)
    {
        characterStateMachine.state = newState;
        animateMachine.Animate(newState);
        switch(newState)
        {
            case CharacterStateMachine.State.Walking : MoveCharacter(); break;
            case CharacterStateMachine.State.Attacking : break;
            case CharacterStateMachine.State.Hurt : StartCoroutine(TrackHurt()); break;
            default: break;
        }
    }
    IEnumerator TrackHurt()
    {
        float t = 0;
        while (t < enemyData.hurtDuration)
        {
            t += Time.deltaTime / enemyData.hurtDuration;
            yield return null;
        }
        if (t >= enemyData.hurtDuration)
        {
            if(enemyHealth.curHealth > 0)
            {
                AttackPlayer();
            }
            else
                ExecuteDeath();
        }
    }
    public void DecideAction()
    {
        CharacterStateFunction(enemyAI.DecideAction(gridAgent));
    }
    public void AttackPlayer()
    {
        
    }
    public void TakeDamage(int damage)
    {
        enemyHealth.curHealth -= damage;
        CharacterStateFunction(CharacterStateMachine.State.Hurt);
    }
    public void ExecuteDeath()
    {
        Enemy.instance.enemySystem.activeEnemies.Remove(this);
        enemyState.state = EnemyState.State.inactive;
        CharacterStateFunction(CharacterStateMachine.State.Dead);
        StartCoroutine(RunDeathAnimation());
    }
    IEnumerator RunDeathAnimation()
    {
        float t = 0;
        animateMachine.Animate(CharacterStateMachine.State.Dead);
        while (t < 1)
        {
            t += Time.deltaTime / deathAnimationDuration;
            yield return null;
        }
        if(t >= 1)
        {
            transform.position = deadSpot;
            Node.instance.ResetNode(gridAgent.node);
        }
    }
}
