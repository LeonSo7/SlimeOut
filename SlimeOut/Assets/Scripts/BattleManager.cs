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

    public Animator slimeRAnimator;
    public Animator slimeGAnimator;
    public Animator slimeBAnimator;

    public Animator oppRAnimator;
    public Animator oppGAnimator;
    public Animator oppBAnimator;

    public Animator skillRAnimator;
    public Animator skillGAnimator;
    public Animator skillBAnimator;

    public GameObject slimeR;
    public GameObject slimeG;
    public GameObject slimeB;
    public GameObject oppR;
    public GameObject oppG;
    public GameObject oppB;

    public static Slime Slime;


    private int petHealth;
    private int oppHealth;
    private int oppDamage;
    private int petDamage;
    private int petLevel;
    public static int oppLevel;
    private int petColor;
    public static int oppColor;

    // Start is called before the first frame update

    void Start()
    {
        Slime = Slime.instance;
        var rnd = new System.Random();
        petHealth = 100;
        oppHealth = 100;
        oppDamage = 0;
        petDamage = 0;
        petLevel = Slime.slimeLvl;
        //petLevel = 10;
        petColor = Slime.colour;
        //petColor = 0;
        //petColor = 1;
        //petColor = 2;
        oppLevel = petLevel + rnd.Next(-2, 2);
        if (oppLevel < 1){oppLevel = 1;}
        oppColor = rnd.Next(0, 3);

        if(petColor==0){triggerSlimeG();}
        else if(petColor==1){triggerSlimeB();}
        else if(petColor==2){triggerSlimeR();}

        if(oppColor==0){triggerOppG();}
        else if(oppColor==1){triggerOppB();}
        else if(oppColor==2){triggerOppR();}

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
        if(petColor==0){slimeGAnimator.SetTrigger("Attack");}
        else if(petColor==1){slimeBAnimator.SetTrigger("Attack");}
        else if(petColor==2){slimeRAnimator.SetTrigger("Attack");}

        petDamage = petLevel*8-oppLevel*3;
        oppDamage = 0;
        StartCoroutine(UpdateState());
        StartCoroutine(oppAttack());
    }

    void Skill()
    {

        if(petColor==0){skillGAnimator.SetTrigger("Skill");slimeGAnimator.SetTrigger("Attack");}
        else if(petColor==1){skillBAnimator.SetTrigger("Skill");slimeBAnimator.SetTrigger("Attack");}
        else if(petColor==2){skillRAnimator.SetTrigger("Skill");slimeRAnimator.SetTrigger("Attack");}

        petDamage = petLevel*10-oppLevel*3;
        oppDamage = 0;
        StartCoroutine(UpdateState());
        StartCoroutine(oppAttack());
    }

    IEnumerator oppAttack()
    {
        yield return new WaitForSeconds (1);
        if(oppColor==0){oppGAnimator.SetTrigger("oppAttack");}
        else if(oppColor==1){oppBAnimator.SetTrigger("oppAttack");}
        else if(oppColor==2){oppRAnimator.SetTrigger("oppAttack");}
        oppDamage = oppLevel*9-petLevel*3;
        petDamage = 0;
        StartCoroutine(UpdateState());
        yield return new WaitForSeconds (1);
        playerTurn();
    }

    void winBattle()
    {
        SceneManager.LoadScene("BattleWinScene");
    }

    void loseBattle()
    {
        SceneManager.LoadScene("BattleLoseScene");
    }

    void triggerSlimeG(){
        slimeG.SetActive(true);


        slimeB.SetActive(false);
        slimeR.SetActive(false);
    }

    void triggerSlimeB(){
        slimeB.SetActive(true);

        slimeG.SetActive(false);
        slimeR.SetActive(false);
    }

    void triggerSlimeR(){
        slimeR.SetActive(true);


        slimeB.SetActive(false);
        slimeG.SetActive(false);
    }

    void triggerOppG(){
        oppG.SetActive(true);


        oppB.SetActive(false);
        oppR.SetActive(false);
    }

    void triggerOppB(){
        oppB.SetActive(true);

        oppG.SetActive(false);
        oppR.SetActive(false);
    }

    void triggerOppR(){
        oppR.SetActive(true);


        oppB.SetActive(false);
        oppG.SetActive(false);
    }
}
