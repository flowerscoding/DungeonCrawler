using TMPro;
using UnityEngine;

public class InventoryBoxState : MonoBehaviour
{
    public InventoryNode activeItemNode;
    public Canvas inventoryCanvas;
    public TextMeshProUGUI itemTypeText;
    public Sprite itemSprite;
    public RectTransform itemHighlight;
    public RectTransform tabHighlight;
    public Vector2[] tabHighlightPositions;
    public GameObject[] pages;
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
                SetCanvas(true);
                break;
            case State.Active:
                state = State.Inactive;
                InputManager.instance.MapChange(InputMapping.MapType.Player);
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
        if (_pageIndex + direction >= 0 && _pageIndex + direction < pages.Length)
            _pageIndex = _pageIndex + direction;
        foreach (GameObject page in pages)
            page.SetActive(page == pages[_pageIndex]);
        tabHighlight.anchoredPosition = tabHighlightPositions[_pageIndex];
        Inventory.Instance.ActiveGridChange(_pageIndex);
    }
}