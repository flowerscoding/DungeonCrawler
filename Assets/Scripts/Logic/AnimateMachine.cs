using UnityEngine;

public class AnimateMachine : MonoBehaviour
{
    public Animator animator;
    public void Animate(CharacterStateMachine.State state)
    {
        switch(state)
        {
            case CharacterStateMachine.State.Idle : animator.Play("Idle"); break;
            case CharacterStateMachine.State.Aggro : animator.Play("Aggro");
            case CharacterStateMachine.State.Walking : animator.Play("Walk"); break;
            case CharacterStateMachine.State.Attacking : animator.Play("Attack"); break;
        }
    }
}
