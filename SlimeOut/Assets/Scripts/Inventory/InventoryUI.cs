using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI instance { get; private set; }
    private void Awake() {
       instance = this;
    }
    public Transform itemHolder;
    public Transform useWindow;
    private Inventory inventory;
    InventorySlot[] slots;
    private Item selectedItem;


    void Start() {
        inventory = Inventory.instance;
        slots = itemHolder.GetComponentsInChildren<InventorySlot>();
        inventory.Add(new Item(Item.ItemType.blue1));
        inventory.Add(new Item(Item.ItemType.blue1));
        inventory.Add(new Item(Item.ItemType.blue3));
        UpdateUI();
    }
    public void Select(Item i) {
        selectedItem = i;
        UpdateText();
    }
    public void Consume() {
        if (selectedItem == null) {
            useWindow.GetComponentInChildren<Text>().text = "Select an item first!";
            return;
        }
        if (inventory.Count(selectedItem) < 1) {
            useWindow.GetComponentInChildren<Text>().text = "You don't have any of this item!";
            return;
        }
        inventory.Remove(selectedItem);
        UpdateUI();
    }
    public void UpdateText() {
        useWindow.GetComponentInChildren<Text>().text = "Item: " + selectedItem.GetDescription() + 
            "\nCost: " + selectedItem.GetCost() +
            "\nYour count: " + inventory.Count(selectedItem) +
            "\nYour balance: " + inventory.balance;
    }
    void UpdateUI () {
        for (int i = 0; i < slots.Length; i++) {
            if (i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
