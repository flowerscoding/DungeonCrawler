using UnityEngine;

public class PlayerLoad : MonoBehaviour
{
    public Transform player;
    public Transform playerHand;
    public Canvas healthCanvas;
    public void LoadPlayer()
    {
        playerHand.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        healthCanvas.enabled = true;
        player.position = Player.instance.gridAgent.node.worldPos;
        Node.instance.DistanceCheck(Player.instance.gridAgent.node);
    }
}
