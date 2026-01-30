using UnityEngine;

public class NpcController : MonoBehaviour
{
    public NpcState npcState;
    public AnimateMachine animateMachine;
    public GridAgent gridAgent;
    public DialogueLines dialogueLines;
    public DialogueTracker dialogueTracker;

    public void StateChange(NpcState.State newState)
    {
        npcState.StateChange(newState, animateMachine);
    }
}
