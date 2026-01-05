using UnityEngine;

public class AnimateMachine : MonoBehaviour
{
    public Animator animator;
    public AnimatorStateInfo info;
    public void Animate(CharacterStateMachine.State state)
    {
        switch(state)
        {
            case CharacterStateMachine.State.Idle : animator.Play("Idle"); break;
            case CharacterStateMachine.State.Aggro : animator.Play("Aggro"); break;
            case CharacterStateMachine.State.Walking : animator.Play("Walk"); break;
            case CharacterStateMachine.State.Attacking : animator.Play("Attack", 0, 0); break;
            case CharacterStateMachine.State.Hurt : animator.Play("Hurt", 0, 0); break;
            case CharacterStateMachine.State.Dead : animator.Play("Dead", 0, 0); break;
            case CharacterStateMachine.State.Push : animator.Play("BoulderPush", 0, 0); break;
            case CharacterStateMachine.State.PushFail : animator.Play("PushFail", 0, 0); break;
            case CharacterStateMachine.State.Running : animator.Play("Run"); break;
            case CharacterStateMachine.State.Block: animator.Play("Block", 0, 0); break;
            case CharacterStateMachine.State.Climb: animator.Play("Climb", 0, 0); break;
            case CharacterStateMachine.State.Destroy: animator.Play("Destroy", 0, 0); break;
        }
    }
    public void ResetMachine()
    {
        animator.speed = 1;
    }
}
