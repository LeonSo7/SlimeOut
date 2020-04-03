using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
	private int _hungerLvl;
	private int _happinessLvl;
	private bool _serverTime;

    // Start is called before the first frame update
    void Start()
    {
        updateState(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateState(){
    	if (PlayerPrefs.HasKey("_happinessLvl")){
    		_happinessLvl = PlayerPrefs.GetInt("_happinessLvl");
    	} else {
    		PlayerPrefs.SetInt("_happinessLvl", 100);
    		_happinessLvl = 100;
    	}

    	if (PlayerPrefs.HasKey("_hungerLvl")){
    		_hungerLvl = PlayerPrefs.GetInt("_hungerLvl");
    	} else {
    		PlayerPrefs.SetInt("_hungerLvl", 100);
    		_hungerLvl = 100;
    	}

    	if(_serverTime){
    		updateServer();
    	}
    }

    void updateServer(){
    	
    }
}
