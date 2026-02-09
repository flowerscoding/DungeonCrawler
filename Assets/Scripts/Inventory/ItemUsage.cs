using UnityEngine;

public class ItemUsage : MonoBehaviour
{
    public bool UseItem(ItemData itemData)
    {
        bool used = false;
        switch(itemData.SubType)
        {
            case "Healing":
                Player.instance.HealPlayer(itemData.HealthAttribute);
                used = true;
                break;
        }
        return used;
    }
}
