using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public GameObject expTxt;
	public GameObject hungerTxt;
	public GameObject lvlTxt;
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
    }
}
