using System.Collections;
using NUnit.Framework.Interfaces;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public ParticleSystem bloodParticles;
    public enum State
    {
        Idle,
        Walking,
        Running,
        Attacking,
        Hurt,
        Dead,
        BoulderPush,
        PushFail,
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
        }
    }
    void PushFailState()
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.PushFail);
        Player.instance.playerInteract.InteractablesOff();
        StartCoroutine(TrackPushFail());
    }
    IEnumerator TrackPushFail()
    {
        yield return null;
        float progress = 0;
        while(progress < 1)
        {
            AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            yield return null;
        }
        if(progress >= 1)
        {
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            NewState(State.Idle);
        }
    }
    void BoulderState()
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Push);
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
        if(progress >= 1)
        {
            NewState(State.Idle);
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        }
    }
    void WalkState()
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Walking);
        Player.instance.playerInteract.InteractablesOff();
    }
    void RunState()
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Running);
        Player.instance.playerInteract.InteractablesOff();
    }
    void AttackState()
    {
        Player.instance.playerInteract.CheckInteractables();
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Attacking);
        StartCoroutine(TrackAttack());
    }
    IEnumerator TrackAttack()
    {
        yield return null;
        PlayerAttack playerAttack = Player.instance.playerAttack;
        float progress = 0;
        bool hitLanded = false;
        while(progress < 1)
        {
            AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
            progress = info.normalizedTime;
            if(progress > Player.instance.playerData.attackHitPoint)
            { 
                if(playerAttack.targetNode.enemyController != null && !hitLanded) //for empty attacks
                {
                    hitLanded = true;
                    playerAttack.targetNode.enemyController.TakeDamage(playerAttack.damageOutput);
                }
            }
            yield return null;
        }
        if(progress >= 1)
        {
            NewState(State.Idle);
            if(playerAttack.targetNode.enemyController == null)
            {
                TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            }
        }
    }
    void IdleState()
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Idle);
        Player.instance.playerInteract.CheckInteractables();
    }
    void DeadState()
    {
        bloodParticles.Play();
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Dead);
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
        if(progress >= 1)
        {
            print("DEAD!!! ADD GAMEOVER FUNCTIONS HERE!");
        }
    }
    void HurtState() //damage already taken in player controller and has been calculated to still survive
    {
        bloodParticles.Play();
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Hurt);
        StartCoroutine(TrackHurt());
    }
    IEnumerator TrackHurt()
    {
        yield return null; //frame pause for new getcurrentstateinfo(0)
        float progress = 0;
        while(progress < 1)
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
}
