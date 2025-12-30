using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public enum State
    {
        Idle,
        Walking,
        Attacking,
        Hurt,
        Dead

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
                Player.instance.animateMachine.Animate(CharacterStateMachine.State.Walking);
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
        }
    }
    void AttackState()
    {
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
                if(playerAttack.targetNode.occupant != null && !hitLanded) //for empty attacks
                {
                    hitLanded = true;
                    playerAttack.targetNode.occupant.TakeDamage(playerAttack.damageOutput);
                }
            }
            yield return null;
        }
        if(progress >= 1)
        {
            NewState(State.Idle);
            if(playerAttack.targetNode.occupant == null)
            {
                TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
            }

        }
    }
    void IdleState()
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Idle);
    }
    void DeadState()
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Dead);
        StartCoroutine(TrackDead());
    }
    IEnumerator TrackDead()
    {
        AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
        yield return null;
        float progress = info.normalizedTime;
        while (progress < 1)
            yield return null;
        if(progress >= 1)
        {
            print("DEAD!!! ADD GAMEOVER FUNCTIONS HERE!");
        }
    }
    void HurtState() //damage already taken in player controller and has been calculated to still survive
    {
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Hurt);
        StartCoroutine(TrackHurt());
    }
    IEnumerator TrackHurt()
    {
        AnimatorStateInfo info = Player.instance.animateMachine.animator.GetCurrentAnimatorStateInfo(0);
        yield return null; //frame pause for new getcurrentstateinfo(0)
        float progress = info.normalizedTime;
        while(progress < 1)
            yield return null;
        if (progress >= 1)
        {
            NewState(State.Idle);
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        }
    }
}
