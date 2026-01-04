using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    
    public ParticleSystem bloodParticles;
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
        controller.enemyMovement.MoveAI(controller);
    }
    void HurtState(EnemyController controller)
    {
        bloodParticles.Play();
        controller.animateMachine.Animate(CharacterStateMachine.State.Hurt);
        StartCoroutine(TrackHurt(controller));
    }
    IEnumerator TrackHurt(EnemyController controller)
    {
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
    public void AttackState(EnemyController controller)
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
        Player.instance.ParryBlockOn();

        controller.animateMachine.Animate(CharacterStateMachine.State.Attacking);



        StartCoroutine(RotateAnimation());

        StartCoroutine(TrackAttack(controller));
    }
    IEnumerator TrackAttack(EnemyController controller)
    {
        yield return null; 
        float progress = 0;
        bool hitLanded = false;
        while (progress < 1)
        {
            AnimatorStateInfo info = controller.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            if(progress > controller.enemyData.parryWindow 
            && progress < controller.enemyData.attackHitFrame
            && Player.instance.playerBlock.parryActive)
            {
                Player.instance.playerBlock.SuccessfulParry(controller);
                Player.instance.playerBlock.BlockOn();
                yield break;
            }
            if (progress > controller.enemyData.attackHitFrame && !hitLanded)
            {
                hitLanded = true;
                Player.instance.TakeDamage(controller.enemyData.attackDamage);
            }

            yield return null;
        }
    }
    IEnumerator RotateAnimation()
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
    void DeadState(EnemyController controller)
    {
        bloodParticles.Play();
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        Enemy.instance.enemySystem.activeEnemies.Remove(controller);
        controller.enemyState.NewState(EnemyState.State.Dead);
        
        controller.animateMachine.Animate(CharacterStateMachine.State.Dead);
        StartCoroutine(TrackDead(controller));
    }
    IEnumerator TrackDead(EnemyController controller)
    {

        yield return null;
        float progress = 0;
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
