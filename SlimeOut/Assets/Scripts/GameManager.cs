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

	public static Slime slime;

    /* COLOURED SLIMES */
    public GameObject slimeR;
    public GameObject slimeG;
    public GameObject slimeB;

    private int _colour;


    public static GameManager instance { get; private set; }

    private void Awake() {
       instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        slime = Slime.instance;

    }

    // Update is called once per frame
    void Update()
    {
        expTxt.GetComponent<Text>().text = "" + slime.expLvl;
        hungerTxt.GetComponent<Text>().text = "" + slime.hungerLvl;
        lvlTxt.GetComponent<Text>().text = "" + slime.slimeLvl;
        nameTxt.GetComponent<Text>().text = slime.name;
        _colour = slime.colour;

        if (_colour == 0){
            triggerSlimeG();
        } else if (_colour == 1){
            triggerSlimeB();
        } else if (_colour == 2){
            triggerSlimeR();
        }
    }

    public void triggerNamePanel(bool active){
        namePanel.SetActive(!namePanel.activeInHierarchy);

        if(active){
            slime.GetComponent<Slime>().name = nameInput.GetComponent<InputField>().text; // Get new name
            PlayerPrefs.SetString("name", slime.name); // Save name to PlayerPrefs
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



}
