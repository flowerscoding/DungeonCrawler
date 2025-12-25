using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
