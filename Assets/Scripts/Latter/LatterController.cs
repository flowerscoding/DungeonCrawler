using UnityEngine;

public class LatterController : MonoBehaviour
{
    public Canvas interactUI;
    public bool active {get; private set;}
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
}
