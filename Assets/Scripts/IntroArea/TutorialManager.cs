using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    [SerializeField] TutorialTransition tutorialTransition;
    [SerializeField] TutorialState tutorialState;
    public TutorialChecklist tutorialChecklist;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void InitiatePhase(TutorialState.State state)
    {
        tutorialState.ChangeState(state);
        tutorialState.ExectuteState();
    }
    public void EndPhase()
    {
        tutorialState.ChangeState(TutorialState.State.Normal);
        tutorialState.ExectuteState();
    }
    public void InitiateTransition(string text)
    {
        tutorialTransition.TransitionText(TutorialTransition.State.View, text);
    }
    public void EndTransition()
    {
        tutorialTransition.TransitionText(TutorialTransition.State.Fade, " ");
    }
    public void ChecklistConfirmed(TutorialChecklist.ConfirmedBool boolName)
    {
        tutorialChecklist.Confirmed(boolName);
    }
    public bool CheckBool(TutorialChecklist.ConfirmedBool confirmedBool)
    {
        return tutorialChecklist.GetBool(confirmedBool);
    }
    public bool CheckState(TutorialState.State state)
    {
        return tutorialState.GetState(state);
    }
}
