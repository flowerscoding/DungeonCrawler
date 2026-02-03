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
        SaveSystem.Instance.LoadSaveData();
        LoadSystem.instance.LoadScene(SaveSystem.Instance.currentData.scene);
    }
}
