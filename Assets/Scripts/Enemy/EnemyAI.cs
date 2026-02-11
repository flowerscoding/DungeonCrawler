using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public ParticleSystem bloodParticles;
    public Canvas healthBarCanvas;
    public Canvas staggerBarCanvas;
    public Image chargeBar;
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
                controller.animateMachine.Animate(AnimateMachine.State.Idle);
                break;
            case State.Aggro:
                controller.animateMachine.Animate(AnimateMachine.State.Aggro);
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
        AnimatorStateInfo state = controller.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
        controller.animateMachine.Animate(AnimateMachine.State.Walk);
        controller.enemyMovement.MoveAI(controller);
    }
    void HurtState(EnemyController controller)
    {
        bloodParticles.Play();
        controller.animateMachine.Animate(AnimateMachine.State.Hurt);
    }
    public void AttackState(EnemyController controller)
    {
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
        Player.instance.ParryBlockOn();

        controller.animateMachine.Animate(AnimateMachine.State.Attack);

        StartCoroutine(RotateAnimation());
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
        if (t > duration)
        {
            transform.rotation = lookRotation;
        }
    }
    void DeadState(EnemyController controller)
    {
        EnableBars(false);
        bloodParticles.Play();
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        Enemy.instance.enemySystem.activeEnemies.Remove(controller);
        controller.enemyState.NewState(EnemyState.State.Dead);

        controller.animateMachine.Animate(AnimateMachine.State.Dead);
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
        progress = 0;
        float deathDuration = 3f;
        while(progress < deathDuration)
        {
            progress += Time.deltaTime;
            yield return null;
        }
            transform.position = controller.deadSpot;
            Node.instance.ResetNode(controller.gridAgent.node);
    }
    public void EnableBars(bool enable)
    {
        healthBarCanvas.enabled = enable;
        staggerBarCanvas.enabled = enable;
        chargeBar.enabled = enable;
    }
}
