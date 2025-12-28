using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GridAgent gridAgent;
    public EnemyAI enemyAI;
    public EnemyMovement enemyMovement;
    public EnemyState enemyState;
    public AnimateMachine animateMachine;
    public CharacterStateMachine characterStateMachine;
    public EnemyHealth enemyHealth;
    public Vector3 deadSpot;
    public float deathAnimationDuration;
    public void MoveCharacter()
    {
        Action<CharacterStateMachine.State> characterStateChange = CharacterStateChange;
        enemyMovement.MoveAI(characterStateChange);
    }
    public void CharacterStateChange(CharacterStateMachine.State newState)
    {
        characterStateMachine.state = newState;
        animateMachine.Animate(newState);
        switch(newState)
        {
            case CharacterStateMachine.State.Walking : MoveCharacter(); break;
            case CharacterStateMachine.State.Attacking : break;
            default: break;
        }
    }
    public void DecideAction()
    {
        CharacterStateChange(enemyAI.DecideAction(gridAgent));
    }
    public void DecideAttack()
    {
        
    }
    public void TakeDamage(int damage)
    {
        enemyHealth.curHealth -= damage;
        if(enemyHealth.curHealth <= 0)
        {
            ExecuteDeath();
        }
    }
    public void ExecuteDeath()
    {
        Enemy.instance.enemySystem.activeEnemies.Remove(this);
        enemyState.state = EnemyState.State.inactive;
        CharacterStateChange(CharacterStateMachine.State.Dead);
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
