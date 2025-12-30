using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyMovement enemyMovement;
    public enum State
    {
        Idle,
        Aggro,
        Walking,
        Attacking,
        Hurt,
        Dead,
    }
    public State state { get; private set; }
    public void NewState(State newState, EnemyController controller)
    {
        state = newState;
        switch (newState)
        {
            case State.Idle:
                controller.animateMachine.Animate(CharacterStateMachine.State.Idle);
                break;
            case State.Aggro:
                controller.animateMachine.Animate(CharacterStateMachine.State.Aggro);
                break;
            case State.Walking:
                WalkState(controller);
                break;
            case State.Attacking:
                AttackState(controller);
                break;
            case State.Hurt:
                HurtState(controller);
                break;
            case State.Dead:
                DeadState(controller);
                break;
        }
    }
    void WalkState(EnemyController controller)
    {
        controller.animateMachine.Animate(CharacterStateMachine.State.Walking);
        enemyMovement.MoveAI(controller);
    }
    void HurtState(EnemyController controller)
    {
        controller.animateMachine.Animate(CharacterStateMachine.State.Hurt);
        StartCoroutine(TrackHurt(controller));
    }
    IEnumerator TrackHurt(EnemyController controller)
    {
        print("HURT");
        yield return null; //wait a frame to help reset the animator
        float progress = 0;
        while (progress < 1)
        {
            AnimatorStateInfo info = controller.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if (progress >= 1)
        {
            NewState(State.Attacking, controller);
        }
    }
    void AttackState(EnemyController controller)
    {
        controller.AttackPlayer();
    }
    void DeadState(EnemyController controller)
    {
        print("DEAD");
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        Enemy.instance.enemySystem.activeEnemies.Remove(controller);
        controller.enemyState.NewState(EnemyState.State.Dead);
        StartCoroutine(TrackDead(controller));
    }
    IEnumerator TrackDead(EnemyController controller)
    {
        yield return null; //wait a frame to help reset the animator
        float progress = 0;
        controller.animateMachine.Animate(CharacterStateMachine.State.Dead);
        while (progress < 1)
        {
            AnimatorStateInfo info = controller.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if (progress >= 1)
        {
            transform.position = controller.deadSpot;
            Node.instance.ResetNode(controller.gridAgent.node);
        }
    }
}
