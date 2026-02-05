using UnityEngine;

public class TransportController : MonoBehaviour
{
    public LoadSystem.Scene sceneDestination;

    public void InitiateTransport()
    {
        LoadSystem.instance.LoadScene(sceneDestination);
    }
}
