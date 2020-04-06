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

	public GameObject slime;

    /* COLOURED SLIMES */
    public GameObject slimeR;
    public GameObject slimeG;
    public GameObject slimeB;
    private int _colour; //0G, 1B, 2R

    public static GameManager instance { get; private set; }

    private void Awake() {
       instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Check if slime colour is exists
        if(! PlayerPrefs.HasKey("colour")){ // No colour
            PlayerPrefs.SetInt("colour", 0); // Default colour
        } else { // Name exists
            _colour = PlayerPrefs.GetInt("colour");
        }
    }

    // Update is called once per frame
    void Update()
    {
        expTxt.GetComponent<Text>().text = "" + slime.GetComponent<Slime>().expLvl;
        hungerTxt.GetComponent<Text>().text = "" + slime.GetComponent<Slime>().hungerLvl;
        lvlTxt.GetComponent<Text>().text = "" + slime.GetComponent<Slime>().slimeLvl;
        nameTxt.GetComponent<Text>().text = slime.GetComponent<Slime>().name;

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
            PlayerPrefs.SetString("name",slime.GetComponent<Slime>().name); // Save name to PlayerPrefs
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

    public int colour {
        get{ return _colour; } // Accessor for colour
        set{ _colour = value; } // Mutator for colour
    }

}
