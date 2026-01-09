using UnityEngine;

public class TutorialDestructible : MonoBehaviour
{

    void OnDisable()
    {
        if (TutorialManager.instance.CheckState(TutorialState.State.Interact))
        {
            TutorialManager.instance.EndPhase();
            TutorialManager.instance.ChecklistConfirmed(TutorialChecklist.ConfirmedBool.Interacted);
        }

        //pause game and show two slides of how attacking works?
    }
}
