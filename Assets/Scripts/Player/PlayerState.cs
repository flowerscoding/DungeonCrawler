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
            print("ATTACK");
                Player.instance.animateMachine.Animate(CharacterStateMachine.State.Attacking);
                break;
            case State.Hurt:
                HurtState();
                break;
            case State.Dead:
                DeadState();
                break;
        }
    }
    void IdleState()
    {
        state = State.Idle;
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Idle);
        Player.instance.playerState.state = State.Idle;
        TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.PlayerTurn);
    }
    void DeadState()
    {
        state = State.Dead;
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Dead);
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
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
        state = State.Hurt;
        Player.instance.animateMachine.Animate(CharacterStateMachine.State.Hurt);
        TurnManager.instance.ChangeTurn(TurnManager.State.Resolving);
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
            TurnManager.instance.ChangeBattleTurn(TurnManager.BattleState.PlayerTurn);
            TurnManager.instance.ChangeTurn(TurnManager.State.PlayerTurn);
        }
    }
}
