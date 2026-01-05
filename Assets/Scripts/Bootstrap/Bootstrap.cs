using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    public DontDestroyManager dontDestroyManager;
    void Awake()
    {
        dontDestroyManager.DontDestroyInitialize();

    }
    void Start()
    {
        LoadSystem.instance.LoadScene(LoadSystem.SceneType.IntroArea);
    }
}
