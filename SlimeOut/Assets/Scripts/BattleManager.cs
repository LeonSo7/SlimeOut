using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public Text petH;
    public Text oppH;
    public Button escapeButton;
    public Button attack;
    public Button skill;

    public Animator petAnimator;
    public Animator oppAnimator;
    public Animator skillAnimator;

    //private Slime Slime = Slime.instance;
    //private Inventory Inventory = Inventory.instance;

    private int petHealth;
    private int oppHealth;
    private int oppDamage;
    private int petDamage;
    private int petLevel;
    private int oppLevel;
    private string petColor;
    private string oppColor;

    private Item[] reward;
    private int income;
    private int exp;
    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();
        petHealth = 100;
        oppHealth = 100;
        oppDamage = 0;
        petDamage = 0;
        //petLevel = Slime.instance.slimeLvl;
        petLevel = 10;
        //petColor = PlayerPrefs.GetString("_slimeColor");
        oppLevel = petLevel + rnd.Next(-2, 2);
        oppColor = ("Blue");
        StartCoroutine(UpdateState());
        playerTurn();
    }

    // Update is called once per frame
    IEnumerator UpdateState()
    {
        if(oppHealth <= 0)
        {
            winBattle();
        }
        if(petHealth <= 0)
        {
            loseBattle();
        }
        petHealth -= oppDamage;
        oppHealth -= petDamage;
        yield return new WaitForSeconds (1);
        petH.GetComponent<Text>().text = petHealth.ToString();
        oppH.GetComponent<Text>().text = oppHealth.ToString();
    }

    void playerTurn()
    {
        escapeButton.onClick.AddListener(loseBattle);
        attack.onClick.AddListener(Attack);
        skill.onClick.AddListener(Skill);
    }

    void Attack()
    {
        petAnimator.SetTrigger("Attack");
        petDamage = petLevel*3-oppLevel*1;
        oppDamage = 0;
        StartCoroutine(UpdateState());
        StartCoroutine(oppAttack());
    }

    void Skill()
    {
        petAnimator.SetTrigger("Skill");
        skillAnimator.SetTrigger("Skill");
        petDamage = petLevel*5-oppLevel*2;
        oppDamage = 0;
        StartCoroutine(UpdateState());
        StartCoroutine(oppAttack());
    }

    IEnumerator oppAttack()
    {
        yield return new WaitForSeconds (1);
        oppAnimator.SetTrigger("oppAttack");
        oppDamage = oppLevel*5-petLevel*3;
        petDamage = 0;
        StartCoroutine(UpdateState());
        yield return new WaitForSeconds (1);
        playerTurn();
    }

    void winBattle()
    {
        reward = new Item[] {new Item(Item.ItemType.blue1), new Item(Item.ItemType.blue1)};
        income = 3;
        exp = oppLevel*10;
        //SceneManager.LoadScene("BattleWinScene");
        //checkout();
    }

    void loseBattle()
    {
        income = 1;
        exp = oppLevel;
        //SceneManager.LoadScene("BattleLoseScene");
        //checkout();
    }

    /*void checkout()
    {
        Inventory.balance += income;
        Slime.expLvl += exp;
        foreach(Item i in reward)
            Inventory.Add(i);
    }*/
}
