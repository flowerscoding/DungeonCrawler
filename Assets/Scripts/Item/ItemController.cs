using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GridAgent gridAgent;
    public ItemData itemData;
    public Vector3 neutralZone;
    public void ObtainItem()
    {
        Inventory.Instance.AddItemToInventory(itemData);
        transform.position = neutralZone;
        gridAgent.ChangeNodeState(NodeClass.State.Empty);
    }
}
