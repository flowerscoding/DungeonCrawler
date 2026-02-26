using UnityEngine;

public class TransportController : MonoBehaviour
{
    public LoadSystem.Scene sceneDestination;
    public string spawnPoint;

    public void InitiateTransport()
    {
        LoadSystem.instance.LoadScene(sceneDestination, spawnPoint);
    }
}
