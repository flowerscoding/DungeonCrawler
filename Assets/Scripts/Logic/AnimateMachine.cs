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
        }
    }
}
