using UnityEngine;

public class TransportController : MonoBehaviour
{
    public LoadSystem.SceneType sceneDestination;

    public void InitiateTransport()
    {
        LoadSystem.instance.LoadScene(sceneDestination);
    }
}
