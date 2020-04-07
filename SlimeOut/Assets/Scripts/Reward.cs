using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reward : MonoBehaviour
{
    public GameObject ItemR1, ItemR2, ItemR3;
    public GameObject ItemG1, ItemG2, ItemG3;
    public GameObject ItemB1, ItemB2, ItemB3;

    private static int oppColor;
    private static int oppLevel;
    private static List<Item> reward;
    private int income;
    private int exp;

    public static Inventory Inventory;
    public static Slime Slime;
    private Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        Slime = Slime.instance;
        oppColor = BattleManager.oppColor;
        oppLevel = BattleManager.oppLevel;
        Inventory = Inventory.instance;
        UpdateState();
    }

    // Update is called once per frame
    void UpdateState()
    {
        if (scene.name == "BattleWinScene")
            winBattle();

        if (scene.name == "BattleLoseScene")
            loseBattle();

        if (oppColor == 0){
            ItemR1.SetActive(false);ItemR2.SetActive(false);ItemR3.SetActive(false);
            ItemG1.SetActive(true);ItemG2.SetActive(true);ItemG3.SetActive(true);
            ItemB1.SetActive(false);ItemB2.SetActive(false);ItemB3.SetActive(false);
        }

        if (oppColor == 1){
            ItemR1.SetActive(false);ItemR2.SetActive(false);ItemR3.SetActive(false);
            ItemG1.SetActive(false);ItemG2.SetActive(false);ItemG3.SetActive(false);
            ItemB1.SetActive(true);ItemB2.SetActive(true);ItemB3.SetActive(true);
        }

        if (oppColor == 2){
            ItemR1.SetActive(true);ItemR2.SetActive(true);ItemR3.SetActive(true);
            ItemG1.SetActive(false);ItemG2.SetActive(false);ItemG3.SetActive(false);
            ItemB1.SetActive(false);ItemB2.SetActive(false);ItemB3.SetActive(false);
        }
    }

    void winBattle()
    {
        reward = new List<Item>();
        if (oppColor == 0){
            reward.Add(new Item(Item.ItemType.green1));
            reward.Add(new Item(Item.ItemType.green1));
            reward.Add(new Item(Item.ItemType.green1));
        }

        if (oppColor == 1){
            reward.Add(new Item(Item.ItemType.blue1));
            reward.Add(new Item(Item.ItemType.blue1));
            reward.Add(new Item(Item.ItemType.blue1));
        }

        if (oppColor == 2){
            reward.Add(new Item(Item.ItemType.red1));
            reward.Add(new Item(Item.ItemType.red1));
            reward.Add(new Item(Item.ItemType.red1));
        }

        income = 10;
        exp = oppLevel*10;
        checkout();
    }

    void loseBattle()
    {
        reward = new List<Item>();
        if (oppColor == 0){
            reward.Add(new Item(Item.ItemType.green1));
        }

        if (oppColor == 1){
            reward.Add(new Item(Item.ItemType.blue1));
        }

        if (oppColor == 2){
            reward.Add(new Item(Item.ItemType.red1));
        }

        income = 5;
        exp = oppLevel*5;
        checkout();
    }

    void checkout()
    {
        Inventory.balance += income;
        Slime.expLvl += exp;
        foreach(Item i in reward)
            Inventory.Add(i);
    }
}
