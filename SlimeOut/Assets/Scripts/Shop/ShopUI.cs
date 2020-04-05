using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopUI : MonoBehaviour
{
    public static ShopUI instance { get; private set; }
    private void Awake() {
       instance = this;
    }
    public Transform itemHolder;
    public Transform buyWindow;
    private Inventory inventory;
    InventorySlot[] slots;
    private int balance;
    private Item selectedItem;


    void Start() {
        inventory = Inventory.instance;
        slots = itemHolder.GetComponentsInChildren<InventorySlot>();
        slots[0].AddItem(new Item(Item.ItemType.red1));
        slots[1].AddItem(new Item(Item.ItemType.red2));
        slots[2].AddItem(new Item(Item.ItemType.red3));
        slots[3].AddItem(new Item(Item.ItemType.green1));
        slots[4].AddItem(new Item(Item.ItemType.green2));
        slots[5].AddItem(new Item(Item.ItemType.green3));
        slots[6].AddItem(new Item(Item.ItemType.blue1));
        slots[7].AddItem(new Item(Item.ItemType.blue2));
        slots[8].AddItem(new Item(Item.ItemType.blue3));
        slots[9].AddItem(new Item(Item.ItemType.xp1));
        slots[10].AddItem(new Item(Item.ItemType.xp2));
        slots[11].AddItem(new Item(Item.ItemType.xp3));
    }

    public void Select(Item i) {
        selectedItem = i;
        UpdateText();
    }
    public void Buy() {
        if (selectedItem == null) {
            buyWindow.GetComponentInChildren<Text>().text = "Select an Item first";
            return;
        }
        int c = selectedItem.GetCost();
        if (inventory.balance >= c) {
            inventory.balance -= c;
            inventory.Add(selectedItem);
            UpdateText();
        }
        else buyWindow.GetComponentInChildren<Text>().text = "Insufficient funds! You need " + (c - inventory.balance) + " more coins!";
    }
    public void Sell() {
        if (selectedItem == null) {
            buyWindow.GetComponentInChildren<Text>().text = "Select an item first!";
            return;
        }
        int c = selectedItem.GetCost();
        if (inventory.Count(selectedItem) < 1) {
            buyWindow.GetComponentInChildren<Text>().text = "You don't have any of this item!";
            return;
        }
        inventory.balance += c;
        inventory.Remove(selectedItem);
        UpdateText();
    }
    public void UpdateText() {
        buyWindow.GetComponentInChildren<Text>().text = "Item: " + selectedItem.GetDescription() + 
            "\nCost: " + selectedItem.GetCost() +
            "\nYour count: " + inventory.Count(selectedItem) +
            "\nYour balance: " + inventory.balance;
    }
}
