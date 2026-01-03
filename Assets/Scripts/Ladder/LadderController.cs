using UnityEngine;

public class LadderController : MonoBehaviour
{
    public Canvas interactUI;
    public bool active {get; private set;}
   [SerializeField] private LoadSystem.SceneType targetScene;
    public void ActivateLatter()
    {
        interactUI.enabled = true;
        active = true;
    }
    public void DeactivateLatter()
    {
        interactUI.enabled = false;
        active = false;
    }
    public LoadSystem.SceneType GetTargetScene()
    {
        return targetScene;
    }
}
