using System;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    public Transform playerHand;
    public Canvas healthCanvas;
    void OnEnable()
    {
        LoadSystem.OnLoad += LoadPlayer;
    }
    void OnDisable()
    {
        LoadSystem.OnLoad -= LoadPlayer;
    }
    void LoadPlayer()
    {
        playerHand.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        healthCanvas.enabled = true;
    }
}
