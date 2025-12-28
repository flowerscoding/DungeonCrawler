using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public PlayerData playerData;
    public PlayerMovement playerMovement;
    public GridAgent gridAgent;
    public PlayerAttack playerAttack;
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
