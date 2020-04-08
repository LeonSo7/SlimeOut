using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MongoDB.Driver;
using MongoDB.Bson;

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
    public string[] ToStringArray () {
        string[] result = new string[items.Count];
        int j = 0;
        foreach (var i in items) result[j++] = i.ToString();
        return result;
    }
    public void setFromStringArray(string[] input) {
        items = new List<Item>();
        for (int i = 0; i < input.Length; i++) items.Add(new Item(input[i]));
    }
    public void OnDeath() {
        items =  new List<Item>();
        balance = 100;
    }
}
