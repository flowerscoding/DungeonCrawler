using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public string ItemName;
    public string ItemType;
    public string SubType;
    public string Description;
    public int HealthAttribute;
    public Sprite ItemSprite;
}
