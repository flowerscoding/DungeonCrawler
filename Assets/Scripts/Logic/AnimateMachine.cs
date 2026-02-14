using UnityEngine;

public class AnimateMachine : MonoBehaviour
{
    public Animator animator;
    public AnimatorStateInfo info;
    public enum State
    {
        Idle,
        Idle2,
        Aggro,
        Walk,
        Attack,
        LightAttack,
        NormalAttack,
        HeavyAttack,
        Hurt,
        Dead,
        Push,
        PushFail,
        Run,
        Block,
        Climb,
        Destroy,
        Pray

    }
    public void Animate(State state)
    {
        switch (state)
        {
            case State.Idle: animator.Play("Idle"); break;
            case State.Idle2: animator.Play("Idle2"); break;
            case State.Aggro: animator.Play("Aggro"); break;
            case State.Walk: animator.Play("Walk"); break;
            case State.Attack: animator.Play("Attack", 0, 0); break;
            case State.Hurt: animator.Play("Hurt", 0, 0); break;
            case State.Dead: animator.Play("Dead", 0, 0); break;
            case State.Push: animator.Play("BoulderPush", 0, 0); break;
            case State.PushFail: animator.Play("PushFail", 0, 0); break;
            case State.Run: animator.Play("Run"); break;
            case State.Block: animator.Play("Block", 0, 0); break;
            case State.Climb: animator.Play("Climb", 0, 0); break;
            case State.Destroy: animator.Play("Destroy", 0, 0); break;
            case State.Pray: animator.Play("Pray", 0, 0); break;
            case State.LightAttack: animator.Play("LightAttack", 0, 0); break;
            case State.NormalAttack: animator.Play("NormalAttack", 0, 0); break;
            case State.HeavyAttack: animator.Play("HeavyAttack", 0, 0); break;
        }
    }
    public void ResetMachine()
    {
        animator.speed = 1;
    }
}
