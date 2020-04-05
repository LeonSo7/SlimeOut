using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
   public static Inventory instance { get; private set; }

   private void Awake() {
       instance = this;
   }

    public List<Item> items = new List<Item>();
    public int balance = 0;
    public void Add (Item item) {
        items.Add(item);
    }
    public void Remove (Item item) {
        items.Remove(item);
    }
    public int Count (Item item) {
        int count = 0;
        foreach (var i in items) {
            if (i == item) count++;
        }
        return count;
    }
}
