using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float stamina;
    public float staminaDrainRate;
    public float staminaGainRate;
    bool _draining;
    bool _gaining;
    bool _coolDown;
    public Image staminaBar;
}
