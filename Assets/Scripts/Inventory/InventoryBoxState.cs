using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class InventoryBoxState : MonoBehaviour
{
    [System.Serializable]
    public class ItemOptions
    {
        public Image itemImage;
        public Image fgImage;
        public Image bgImage;
        public Image borderImage;
        public Image buttonsBGImage;
        public Image cancelImage;
        public Image useImage;
        public Image discardImage;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI useText;
        public TextMeshProUGUI discardText;
        public TextMeshProUGUI cancelText;
        public Image buttonHighlight;
    };
    [System.Serializable]
    public class SidePanel
    {
        public Image descriptionBorder;
        public Image descriptionBG;
        public Image descriptionFG;
        public TextMeshProUGUI itemName;
        public Image itemBorder;
        public Image itemBG;
        public Image itemImg;
        public TextMeshProUGUI descriptionText;
        public TextMeshProUGUI itemSubTypeText;
    }
    public SidePanel sidePanel;

    public ItemOptions itemOptions;
    InventoryNode _interactNode;
    public Image[] itemSprites;
    public InventoryNode activeItemNode;
    public Canvas inventoryCanvas;
    public TextMeshProUGUI itemTypeText;
    public Image itemDescriptionImage;
    public RectTransform itemHighlight;
    public RectTransform tabHighlight;
    public Vector2[] tabHighlightPositions;
    int _pageIndex;
    public enum State
    {
        Inactive,
        Active,
    }
    enum InventoryPhase
    {
        Box,
        ItemOptions,
    }
    enum ItemOptionButton
    {
        Use,
        Discard,
        Cancel,
    }
    ItemOptionButton _itemOptionButton;
    int _itemOptionButtonEnumSize = 3;
    InventoryPhase _phase;
    State state;
    public int xLength;
    public int yLength;
    int xIndex;
    int yIndex;
    public void InventoryToggled()
    {
        EnableItemOptionsPanel(false);
        ResetInteractNode();
        ResetTab();
        ResetItemOptionButton();
        ChangePhase(InventoryPhase.Box);
        ChangeActiveOptionButton(ItemOptionButton.Use);
        ItemOptionSelectChange(0);
        switch (state)
        {
            case State.Inactive:
                state = State.Active;
                InputManager.instance.MapChange(InputMapping.MapType.InventoryMenu);
                _pageIndex = 0;
                FillItemBoxes();
                FillSidePanel();
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
        if (_phase == InventoryPhase.ItemOptions)
        {

            return;
        }
        if (xIndex + direction <= xLength && xIndex + direction >= 0
        && Inventory.Instance.grid.grid[yIndex, xIndex + direction].itemData != null)
        {
            xIndex = xIndex + direction;
            itemHighlight.anchoredPosition += new Vector2(direction > 0 ? 250 : -250, 0);
            _interactNode = Inventory.Instance.grid.grid[yIndex, xIndex];
            FillSidePanel();
        }
    }
    public void YChange(int direction)
    {
        if (_phase == InventoryPhase.ItemOptions)
        {
            ItemOptionSelectChange(direction);
            return;
        }
        if (yIndex + direction <= yLength && yIndex + direction >= 0
        && Inventory.Instance.grid.grid[yIndex + direction, xIndex].itemData != null)
        {
            yIndex = yIndex + direction;
            itemHighlight.anchoredPosition += new Vector2(0, direction > 0 ? -200 : 200);
            _interactNode = Inventory.Instance.grid.grid[yIndex, xIndex];
            FillSidePanel();
        }
    }
    void ItemOptionSelectChange(int direction)
    {
        if (_itemOptionButton + direction >= 0 && (int)_itemOptionButton + direction < _itemOptionButtonEnumSize)
            _itemOptionButton = _itemOptionButton + direction;
        switch (_itemOptionButton)
        {
            case ItemOptionButton.Use:
                itemOptions.buttonHighlight.rectTransform.anchoredPosition = new Vector2(0, -80);
                break;
            case ItemOptionButton.Discard:
                itemOptions.buttonHighlight.rectTransform.anchoredPosition = new Vector2(0, -140);
                break;
            case ItemOptionButton.Cancel:
                itemOptions.buttonHighlight.rectTransform.anchoredPosition = new Vector2(0, -200);
                break;
            default:
                break;
        }
    }
    void ResetItemOptionButton()
    {
        _itemOptionButton = ItemOptionButton.Use;
        ItemOptionSelectChange(0);
    }
    public void TabSwitch(int direction)
    {
        if (_phase != InventoryPhase.Box) return;
        if (_pageIndex + direction >= 0 && _pageIndex + direction < 3)
            _pageIndex = _pageIndex + direction;
        tabHighlight.anchoredPosition = tabHighlightPositions[_pageIndex];
        FillItemBoxes();
        FillSidePanel();
        ResetInteractNode();
    }
    void ResetInteractNode()
    {
        xIndex = 0;
        yIndex = 0;
        _interactNode = Inventory.Instance.grid.grid[0, 0];
        itemHighlight.anchoredPosition = new Vector2(-250, 160);
    }
    void ResetTab()
    {
        tabHighlight.anchoredPosition = new Vector2(-300, 360);
    }
    void EnableItemOptionsPanel(bool enable)
    {
        itemOptions.itemImage.enabled = enable;
        itemOptions.fgImage.enabled = enable;
        itemOptions.buttonsBGImage.enabled = enable;
        itemOptions.bgImage.enabled = enable;
        itemOptions.borderImage.enabled = enable;
        itemOptions.cancelImage.enabled = enable;
        itemOptions.useImage.enabled = enable;
        itemOptions.discardImage.enabled = enable;
        itemOptions.itemName.enabled = enable;
        itemOptions.useText.enabled = enable;
        itemOptions.discardText.enabled = enable;
        itemOptions.cancelText.enabled = enable;
        itemOptions.buttonHighlight.enabled = enable;
    }
    void EnableSidePanel(bool enable)
    {
        sidePanel.descriptionBorder.enabled = enable;
        sidePanel.descriptionBG.enabled = enable;
        sidePanel.descriptionFG.enabled = enable;
        sidePanel.itemName.enabled = enable;
        sidePanel.itemBorder.enabled = enable;
        sidePanel.itemBG.enabled = enable;
        sidePanel.itemImg.enabled = enable;
        sidePanel.descriptionText.enabled = enable;
        sidePanel.itemSubTypeText.enabled = enable;
    }
    void FillSidePanel()
    {
        if(_interactNode == null || _interactNode.itemData == null)
        {
            EnableSidePanel(false);
            return;
        }
        EnableSidePanel(true);
        sidePanel.itemName.text = _interactNode.itemData.ItemName;
        sidePanel.descriptionText.text = _interactNode.itemData.Description;
        sidePanel.itemSubTypeText.text = _interactNode.itemData.SubType;
        sidePanel.itemImg.sprite = _interactNode.itemData.ItemSprite;
    }
    void FillItemBoxes()
    {
        int i = 0;
        switch (_pageIndex)
        {
            case 0:
                Inventory.Instance.SetGrid("General");
                foreach (InventoryNode node in Inventory.Instance.grid.grid)
                {
                    if (node.itemData != null)
                    {
                        itemSprites[i].sprite = node.itemData.ItemSprite;
                        Color c = itemSprites[i].color;
                        c.a = 1;
                        itemSprites[i].color = c;
                    }
                    else
                    {
                        itemSprites[i].sprite = null;
                        Color c = itemSprites[i].color;
                        c.a = 0;
                        itemSprites[i].color = c;
                    }
                    i++;
                }
                break;
            case 1:
                Inventory.Instance.SetGrid("Armor");
                foreach (InventoryNode node in Inventory.Instance.grid.grid)
                {
                    if (node.itemData != null)
                    {
                        itemSprites[i].sprite = node.itemData.ItemSprite;
                        Color c = itemSprites[i].color;
                        c.a = 1;
                        itemSprites[i].color = c;
                    }
                    else
                    {
                        itemSprites[i].sprite = null;
                        Color c = itemSprites[i].color;
                        c.a = 0;
                        itemSprites[i].color = c;
                    }
                    i++;
                }
                break;
            case 2:
                Inventory.Instance.SetGrid("Key");
                foreach (InventoryNode node in Inventory.Instance.grid.grid)
                {
                    if (node.itemData != null)
                    {
                        itemSprites[i].sprite = node.itemData.ItemSprite;
                        Color c = itemSprites[i].color;
                        c.a = 1;
                        itemSprites[i].color = c;
                    }
                    else
                    {
                        itemSprites[i].sprite = null;
                        Color c = itemSprites[i].color;
                        c.a = 0;
                        itemSprites[i].color = c;
                    }
                    i++;
                }
                break;
        }
    }
    public void InteractWithNode()
    {
        switch (_phase)
        {
            case InventoryPhase.Box:
                if (_interactNode.itemData != null)
                {
                    ChangePhase(InventoryPhase.ItemOptions);
                    EnableItemOptionsPanel(true);
                    itemOptions.itemName.text = _interactNode.itemData.ItemName;
                    itemOptions.itemImage.sprite = _interactNode.itemData.ItemSprite;
                }
                break;
            case InventoryPhase.ItemOptions:
                switch (_itemOptionButton)
                {
                    case ItemOptionButton.Use:
                        UseItem(_interactNode.itemData);
                        break;
                    case ItemOptionButton.Discard:
                        DiscardItem();
                        break;
                    case ItemOptionButton.Cancel:
                        CancelItemOptions();
                        break;
                    default: break;
                }
                break;
        }
    }
    void ChangePhase(InventoryPhase newPhase)
    {
        _phase = newPhase;
    }
    void ChangeActiveOptionButton(ItemOptionButton button)
    {
        _itemOptionButton = button;
    }
    void UseItem(ItemData itemData)
    {
        EnableItemOptionsPanel(false);
        ChangePhase(InventoryPhase.Box);
        if (Player.instance.UseItem(itemData))
        {
            Inventory.Instance.RemoveFromInventory(itemData);
            ResetItemOptionButton();
            ResetInteractNode();
            FillItemBoxes();
            FillSidePanel();
        }
    }
    void DiscardItem()
    {
        Inventory.Instance.RemoveFromInventory(_interactNode.itemData);
        EnableItemOptionsPanel(false);
        ChangePhase(InventoryPhase.Box);
        ResetItemOptionButton();
        ResetInteractNode();
        FillItemBoxes();
        FillSidePanel();
    }
    void CancelItemOptions()
    {
        EnableItemOptionsPanel(false);
        ChangePhase(InventoryPhase.Box);
        ResetItemOptionButton();
    }
}