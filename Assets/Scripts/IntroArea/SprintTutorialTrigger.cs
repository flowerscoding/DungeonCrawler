using UnityEngine;

public class SprintTutorialTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.TryGetComponent(out PlayerGridAgent gridAgent))
        {
            if(!TutorialManager.instance.tutorialChecklist.sprinted)
            {
                if(TutorialManager.instance.CheckBool(TutorialChecklist.ConfirmedBool.Sprinted) == false)
                TutorialManager.instance.InitiatePhase(TutorialState.State.Sprint);
            }
        }
    }
}
