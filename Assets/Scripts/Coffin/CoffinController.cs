using UnityEngine;

public class CoffinController : MonoBehaviour
{
    [SerializeField] Canvas interactCanvas;

    public void InteractablesOn()
    {
        interactCanvas.enabled = true;
    }
    public void InteractablesOff()
    {
        interactCanvas.enabled = false;
    }
}
