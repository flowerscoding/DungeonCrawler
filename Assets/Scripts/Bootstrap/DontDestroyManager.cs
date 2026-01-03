using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{

    public GameObject[] dontDestroys;
    public void DontDestroyInitialize()
    {
        foreach(GameObject obj in dontDestroys)
        {
            DontDestroyOnLoad(obj);
        }
    }
}
