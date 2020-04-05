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

    public int petHealth;
    public int oppHealth;
    public int oppDamage;
    public int petDamage;
    public int petLevel;
    public int oppLevel;
    public string petColor;
    public string oppColor;

    public string[] reward;
    public int income;
    public int exp;
    // Start is called before the first frame update
    void Start()
    {
        var rnd = new System.Random();
        petHealth = 100;
        oppHealth = 100;
        oppDamage = 0;
        petDamage = 0;
        //petLevel = PlayerPrefs.GetInt("_slimeLvl");
        petLevel = 10;
        petColor = PlayerPrefs.GetString("_slimeColor");
        oppLevel = petLevel + rnd.Next(-2, 2);
        oppColor = ("Purple");
        UpdateState();
        playerTurn();
    }

    // Update is called once per frame
    void UpdateState()
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
        petDamage = petLevel*5-oppLevel*2;
        oppDamage = 0;
        UpdateState();
        StartCoroutine(oppAttack());
    }

    void Skill()
    {
        petAnimator.SetTrigger("Attack");
        petDamage = petLevel*8-oppLevel*3;
        oppDamage = 0;
        UpdateState();
        StartCoroutine(oppAttack());
    }

    IEnumerator oppAttack()
    {
        yield return new WaitForSeconds (1);
        oppAnimator.SetTrigger("oppAttack");
        oppDamage = oppLevel*5-petLevel*3;
        petDamage = 0;
        UpdateState();
        yield return new WaitForSeconds (1);
        playerTurn();
    }

    void winBattle()
    {
        reward = new string[] { "", "", "" };
        income = 3;
        exp = oppLevel*10;
        //SceneManager.LoadScene("BattleWinScene");
        //checkout();
    }

    void loseBattle()
    {
        reward = new string[] {""};
        income = 1;
        exp = oppLevel;
        //SceneManager.LoadScene("BattleLoseScene");
        //checkout();
    }

    /*void checkout()
    {
        PlayerPrefs.set("_expLvl", + exp);
        PlayerPrefs.set("_currLvl", + income);
        PlayerPrefs.set("_items", append(reward));
    }*/
}
