using System;
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
            case CharacterStateMachine.State.Attacking: print("ATTACK"); break;
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
    }
}
