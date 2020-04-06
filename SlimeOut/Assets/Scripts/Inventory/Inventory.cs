using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance { get; private set; }

    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad (gameObject);
            instance = this;
        } 
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public List<Item> items = new List<Item>();
    public int balance = 0;
    public bool Add (Item item) {
        if (items.Count > 19) {
            return false;
        }
        items.Add(item);
        return true;
    }
    public void Remove (Item item) {
        foreach (var i in items) {
            if (item.type == i.type) {
                items.Remove(i);
                return;
            }
        }
    }
    public int Count (Item item) {
        int count = 0;
        foreach (var i in items) {
            if (item.type == i.type) count++;
        }
        return count;
    }
}
