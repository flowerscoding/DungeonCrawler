using UnityEngine;

public class LadderController : MonoBehaviour
{
    public Canvas interactUI;
    public bool active {get; private set;}
   [SerializeField] private LoadSystem.Scene targetScene;
    public void ActivateLadder()
    {
        interactUI.enabled = true;
        active = true;
    }
    public void DeactivateLadder()
    {
        interactUI.enabled = false;
        active = false;
    }
    public LoadSystem.Scene GetTargetScene()
    {
        return targetScene;
    }
}
