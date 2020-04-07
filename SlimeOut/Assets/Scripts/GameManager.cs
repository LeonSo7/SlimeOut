using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /* SLIME STATS OBJECTS*/
	public GameObject expTxt;
	public GameObject hungerTxt;
	public GameObject lvlTxt;

    /* SLIME NAME OBJECTS */
    public GameObject namePanel;
    public GameObject nameInput;
    public GameObject nameTxt;

    /* REVIVE PANEL */
    public GameObject revivePanel;

	public static Slime slime;
    public static Inventory inventory;

    /* COLOURED SLIMES */
    public GameObject slimeR;
    public GameObject slimeG;
    public GameObject slimeB;

    /* DEAD SLIMES */
    public GameObject slimeRDead;
    public GameObject slimeGDead;
    public GameObject slimeBDead;

    private int _colour;
    private int _hunger;


    public static GameManager instance { get; private set; }

    private void Awake() {
       instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        slime = Slime.instance;
        inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        expTxt.GetComponent<Text>().text = "" + slime.expLvl;
        hungerTxt.GetComponent<Text>().text = "" + slime.hungerLvl;
        lvlTxt.GetComponent<Text>().text = "" + slime.slimeLvl;
        nameTxt.GetComponent<Text>().text = slime.name;
        _colour = slime.colour;
        _hunger = slime.hungerLvl;

        if (_colour == 0 && _hunger > 0){
            triggerSlimeG();
            triggerAllDeadInactive();
        } else if (_colour == 1 && _hunger > 0){
            triggerSlimeB();
            triggerAllDeadInactive();
        } else if (_colour == 2 && _hunger > 0){
            triggerSlimeR();
            triggerAllDeadInactive();
        } else if (_colour == 0 && _hunger <= 0){
            triggerSlimeGDead();
            triggerAllAliveInactive();
            triggerDead();
        } else if (_colour == 1 && _hunger <= 0){
            triggerSlimeBDead();
            triggerAllAliveInactive();
            triggerDead();
        } else if (_colour == 2 && _hunger <= 0){
            triggerSlimeRDead();
            triggerAllAliveInactive();
            triggerDead();
        } 
    }

    public void triggerNamePanel(bool active){
        namePanel.SetActive(!namePanel.activeInHierarchy);

        if(active){
            slime.name = nameInput.GetComponent<InputField>().text; // Get new name
        }
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

    void triggerAllDeadInactive(){
        slimeGDead.SetActive(false);
        slimeBDead.SetActive(false);
        slimeRDead.SetActive(false);
    }

    void triggerAllAliveInactive(){
        slimeR.SetActive(false);
        slimeB.SetActive(false);
        slimeG.SetActive(false);
    }

    void triggerSlimeGDead(){
        slimeGDead.SetActive(true);
        slimeBDead.SetActive(false);
        slimeRDead.SetActive(false);
    }

    void triggerSlimeBDead(){
        slimeBDead.SetActive(true);
        slimeGDead.SetActive(false);
        slimeRDead.SetActive(false);
    }

    void triggerSlimeRDead(){
        slimeRDead.SetActive(true);
        slimeBDead.SetActive(false);
        slimeGDead.SetActive(false);
    }

    void triggerDead(){
        revivePanel.SetActive(true);
    }

    public void triggerRevive(bool active){
        revivePanel.SetActive(!revivePanel.activeInHierarchy);

        if(active){
            slime.hungerLvl = 100;
            slime.expLvl = 0;
            slime.slimeLvl = 1;
            inventory.OnDeath();
        }
    }


}
