using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerMovement playerMovement;
    public PlayerNode playerNode;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(instance);
            return;
        }
        instance = this;
    }
}
