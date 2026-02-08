using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemData itemData;
    public Vector3 neutralZone;
    public void ObtainItem()
    {
        Inventory.Instance.AddItemToGrid(itemData);
        transform.position = neutralZone;
    }
}
