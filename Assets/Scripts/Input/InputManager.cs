using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;
    public PlayerInputActions inputActions;

    public InputMapping inputMapping;
    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        inputActions = new PlayerInputActions();
        inputActions.Disable();
        inputActions.asset.actionMaps[0].Enable();
    }
    public void MapChange(string newMap)
    {
        inputMapping.MapChange(newMap);
    }
}
