using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Button selectButton;
    private Inventory inventory = Inventory.instance;
    Item item;
    public void AddItem(Item newItem) {
        item = newItem;
        icon.sprite = newItem.GetSprite();
        icon.enabled = true;
    }
    public void ClearSlot() {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
    public void Select() {
        ShopUI.instance.Select(item);
    }
    public void SelectInv() {
        InventoryUI.instance.Select(item);
    }
}
