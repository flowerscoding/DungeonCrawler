using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class PlayerState : MonoBehaviour
{
    public ParticleSystem bloodParticles;
    public Transform playerHand;
    public enum State
    {
        Idle,
        Walking,
        Running,
        Attacking,
        StaggerAttacking,
        Block,
        Hurt,
        Dead,
        BoulderPush,
        PushFail,
        Climb,
        Destroy,
        Pray,
        Dialogue,
    }
    public State state { get; private set; }
    public void NewState(State newState)
    {
        state = newState;
        switch (newState)
        {
            case State.Idle:
                IdleState();
                break;
            case State.Walking:
                WalkState();
                break;
            case State.Running:
                RunState();
                break;
            case State.Attacking:
                AttackState();
                break;
            case State.StaggerAttacking:
            StaggerAttackState();
                break;
            case State.Hurt:
                HurtState();
                break;
            case State.Dead:
                DeadState();
                break;
            case State.BoulderPush:
                BoulderState();
                break;
            case State.PushFail:
                PushFailState();
                break;
            case State.Block:
                BlockState();
                break;
            case State.Climb:
                ClimbState();
                break;
            case State.Destroy:
                DestroyState();
                break;
            case State.Pray:
                PrayState();
                break;
        }
    }
    void PrayState()
    {
        Player.instance.animateMachine.Animate(AnimateMachine.State.Pray);
    }
    void DestroyState()
    {
        Player.instance.animateMachine.Animate(AnimateMachine.State.Destroy);
        StartCoroutine(TrackDestroy());
    }
    IEnumerator TrackDestroy()
    {
        yield return null;
        float progress = 0;
        NodeClass destructibleNode = Player.instance.playerInteract.curInteractingNode;
        bool hitLanded = false;
        while (progress < 1)
        {
            AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            if(progress > Player.instance.playerData.destroyHitPoint && !hitLanded)
            {
                hitLanded = true;
                destructibleNode.destructibleController.DestroyDestructible();
            } 
            yield return null;
        }
        Player.instance.animateMachine.Animate(AnimateMachine.State.Idle);
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
    }
    void ClimbState()
    {
        Player.instance.animateMachine.Animate(AnimateMachine.State.Climb);
        playerHand.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }
    void BlockState()
    {
        Player.instance.animateMachine.Animate(AnimateMachine.State.Block);
        StartCoroutine(TrackBlock());
    }
    IEnumerator TrackBlock()
    {
        yield return null;
        float progress = 0;
        while (progress < 1)
        {
            AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;

            if (progress > 0.9f && Player.instance.playerBlock.holdingBlock)
            {
                Player.instance.animateMachine.animator.speed = 0f;
                yield break;
            }
            yield return null;
        }
    }
    void PushFailState()
    {
        Player.instance.playerBlock.BlockOff();

        Player.instance.animateMachine.Animate(AnimateMachine.State.PushFail);
        Player.instance.playerInteract.InteractablesOff();
        StartCoroutine(TrackPushFail());
    }
    IEnumerator TrackPushFail()
    {
        yield return null;
        float progress = 0;
        while (progress < 1)
        {
            AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if (progress >= 1)
        {
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            NewState(State.Idle);
        }
    }
    void BoulderState()
    {
        Player.instance.playerBlock.BlockOff();

        Player.instance.animateMachine.Animate(AnimateMachine.State.Push);
        Player.instance.playerInteract.InteractablesOff();
        StartCoroutine(TrackBoulderPush());
    }
    IEnumerator TrackBoulderPush()
    {
        yield return null;
        float progress = 0;
        while (progress < 1)
        {
            AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if (progress >= 1)
        {
            NewState(State.Idle);
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        }
    }
    void WalkState()
    {
        Player.instance.animateMachine.ResetMachine();

        Player.instance.animateMachine.Animate(AnimateMachine.State.Walk);
        Player.instance.playerInteract.InteractablesOff();
    }
    void RunState()
    {
        Player.instance.animateMachine.ResetMachine();

        Player.instance.animateMachine.Animate(AnimateMachine.State.Run);
        Player.instance.playerInteract.InteractablesOff();
    }
    void AttackState()
    {
        SaveSystem.Instance.SaveGame();
        
        Player.instance.playerBlock.BlockOff();

        Player.instance.playerInteract.CheckInteractables();
        Player.instance.animateMachine.Animate(AnimateMachine.State.Attack);
    }
    void StaggerAttackState()
    {
        SaveSystem.Instance.SaveGame();
        
        Player.instance.playerBlock.BlockOff();

        Player.instance.playerInteract.CheckInteractables();
        Player.instance.animateMachine.Animate(AnimateMachine.State.StaggerAttack);
    }
    void IdleState()
    {
        Player.instance.animateMachine.Animate(AnimateMachine.State.Idle);
        Player.instance.playerInteract.CheckInteractables();
    }
    void DeadState()
    {
        Player.instance.OccludePlayer(false);

        Player.instance.animateMachine.ResetMachine();
        Player.instance.playerBlock.BlockOff();

        bloodParticles.Play();
        Player.instance.animateMachine.Animate(AnimateMachine.State.Dead);
        StartCoroutine(TrackDead());
    }
    IEnumerator TrackDead()
    {
        yield return null;
        float progress = 0;
        while (progress < 1)
        {
            AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if (progress >= 1)
        {
            Player.instance.PlayerDied();
        }
    }
    void HurtState() //damage already taken in player controller and has been calculated to still survive
    {
        Player.instance.OccludePlayer(false);

        Player.instance.animateMachine.ResetMachine();
        Player.instance.playerBlock.BlockOff();

        bloodParticles.Play();
        Player.instance.animateMachine.Animate(AnimateMachine.State.Hurt);
    }
}
