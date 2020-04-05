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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        expTxt.GetComponent<Text>().text = "" + slime.GetComponent<Slime>().expLvl;
        hungerTxt.GetComponent<Text>().text = "" + slime.GetComponent<Slime>().hungerLvl;
        lvlTxt.GetComponent<Text>().text = "" + slime.GetComponent<Slime>().slimeLvl;
        nameTxt.GetComponent<Text>().text = slime.GetComponent<Slime>().name;
    }

    public void triggerNamePanel(bool active){
        namePanel.SetActive(!namePanel.activeInHierarchy);

        if(active){
            slime.GetComponent<Slime>().name = nameInput.GetComponent<InputField>().text; // Get new name
            PlayerPrefs.SetString("name",slime.GetComponent<Slime>().name); // Save name to PlayerPrefs
        }
    }
}
