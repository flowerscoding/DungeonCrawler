using System;
using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    public Transform playerHand;

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
    }
}
