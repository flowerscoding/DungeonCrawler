using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialChecklist : MonoBehaviour
{
    public bool sprinted;
    public bool interacted;
    public bool parried;
    public bool itemPickedUp;

    public enum ConfirmedBool
    {
        Sprinted,
        Interacted,
        Parried,
        ItemPickedUp,
    }
    public void Confirmed(ConfirmedBool confirmedBool)
    {
        switch (confirmedBool)
        {
            case ConfirmedBool.Sprinted: sprinted = true; break;
            case ConfirmedBool.Interacted: interacted = true; break;
            case ConfirmedBool.Parried: parried = true; break;
            case ConfirmedBool.ItemPickedUp: itemPickedUp = true; break;
        }
    }
    public bool GetBool(ConfirmedBool confirmedBool)
    {
        bool boolState;
        switch (confirmedBool)
        {
            case ConfirmedBool.Sprinted: boolState = sprinted; break;
            case ConfirmedBool.Interacted: boolState = interacted; break;
            case ConfirmedBool.Parried: boolState = parried; break;
            case ConfirmedBool.ItemPickedUp: boolState = itemPickedUp; break;
            default: boolState = false; break;
        }
        return boolState;
    }
}