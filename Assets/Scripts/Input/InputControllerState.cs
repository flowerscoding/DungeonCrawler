using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static event Action ControllerChanged;
    public enum Type
    {
        Keyboard,
        Gamepad,
    }
    public Type state;
    public void ChangeController(Type newState)
    {
        state = newState;

        ControllerChanged?.Invoke();
    }
}
