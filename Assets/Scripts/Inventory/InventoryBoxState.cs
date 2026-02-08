using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryBoxState : MonoBehaviour
{
    public Image[] itemSprites;
    public InventoryNode activeItemNode;
    public Canvas inventoryCanvas;
    public TextMeshProUGUI itemTypeText;
    public Image itemDescriptionImage;
    public RectTransform itemHighlight;
    public RectTransform tabHighlight;
    public Vector2[] tabHighlightPositions;
    private int _pageIndex;
    public enum State
    {
        Inactive,
        Active,
    }
    public class ItemPage
    {
        public ItemData itemData;
    }
    private State state;
    public int xLength;
    public int yLength;
    private int xIndex;
    private int yIndex;
    public void InventoryToggled()
    {
        switch (state)
        {
            case State.Inactive:
                state = State.Active;
                InputManager.instance.MapChange(InputMapping.MapType.InventoryMenu);
                _pageIndex = 0;
                FillItemBoxes();
                SetCanvas(true);
                break;
            case State.Active:
                state = State.Inactive;
                InputManager.instance.MapChange(InputMapping.MapType.Player);
                _pageIndex = 0;
                SetCanvas(false);
                break;
        }
    }
    void SetCanvas(bool active)
    {
        inventoryCanvas.enabled = active;
        xIndex = 0;
        yIndex = 0;
    }
    public void XChange(int direction)
    {
        if (xIndex + direction <= xLength && xIndex + direction >= 0)
        {
            xIndex = xIndex + direction;
            itemHighlight.anchoredPosition += new Vector2(direction > 0 ? 250 : -250, 0);
        }
    }
    public void YChange(int direction)
    {
        if (yIndex + direction <= yLength && yIndex + direction >= 0)
        {
            yIndex = yIndex + direction;
            itemHighlight.anchoredPosition += new Vector2(0, direction > 0 ? -200 : 200);
        }
    }
    public void TabChange(int direction)
    {
        if (_pageIndex + direction >= 0 && _pageIndex + direction < 3)
            _pageIndex = _pageIndex + direction;
        tabHighlight.anchoredPosition = tabHighlightPositions[_pageIndex];
        FillItemBoxes();
    }
    void FillItemBoxes()
    {
        int i = 0;
        switch (_pageIndex)
        {
            case 0:
                Inventory.Instance.SetGrid("General");
                foreach(InventoryNode node in Inventory.Instance.grid.grid)
                {
                    itemSprites[i].sprite = node.itemData.ItemSprite;
                    Color c = itemSprites[i].color;
                    c.a = 1;
                    itemSprites[i].color = c;
                    i++;
                }
                break;
            case 1:
                Inventory.Instance.SetGrid("Armor");
                break;
            case 2:
                Inventory.Instance.SetGrid("Key");
                break;
        }
    }
}