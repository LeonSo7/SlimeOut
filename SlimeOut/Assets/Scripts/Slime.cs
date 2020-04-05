using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Slime : MonoBehaviour
{
	[SerializeField]
	private int _hungerLvl;
	[SerializeField]
	private int _expLvl;
    [SerializeField]
    private int _slimeLvl;
	private bool _serverTime;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetString("playerTime", "04/05/2020 9:00:12");
        updateState(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateState(){
    	if (PlayerPrefs.HasKey("_expLvl")){
    		_expLvl = PlayerPrefs.GetInt("_expLvl");
    	} else { // Default state - exp
    		_expLvl = 0;
    		PlayerPrefs.SetInt("_expLvl", _expLvl);
    	}

    	if (PlayerPrefs.HasKey("_hungerLvl")){
    		_hungerLvl = PlayerPrefs.GetInt("_hungerLvl");
    	} else { // Default state - hunger
    		_hungerLvl = 100;
    		PlayerPrefs.SetInt("_hungerLvl", _hungerLvl);
    	}

        if (PlayerPrefs.HasKey("_slimeLvl")){
            _slimeLvl = PlayerPrefs.GetInt("_slimeLvl");
        } else { // Default state - slime level
            _slimeLvl = 1;
            PlayerPrefs.SetInt("_slimeLvl", _expLvl);
        }

        if (!PlayerPrefs.HasKey("playerTime")){
            setPlayerTime();
        }

        TimeSpan timeDiff = getTimeDiff();
        _hungerLvl  -= (int)(timeDiff.TotalHours * 4); // Subtract 4 from hunger every hour
        if (_hungerLvl  < 0){
            _hungerLvl = 0; // Set hunger level to 0 if falls below 0
        }


        // Use server time if available, else use device time
    	if(_serverTime){
    		updateServer();
    	} else {
            InvokeRepeating("updateDevice", 0f, 30f); // Update time from device every 30s
        }
    }

    void updateServer(){

    }

    void updateDevice(){
        setPlayerTime();
    }

    void setPlayerTime(){
        PlayerPrefs.SetString("playerTime", getTimeStr());
    }

    string getTimeStr(){
        DateTime now = DateTime.Now; // Get current time on system
        string dateFormat = now.Month + "/" + now.Day + "/" + now.Year + " " + now.Hour + ":" + now.Minute + ":" + now.Second;
        return dateFormat;
    }

    TimeSpan getTimeDiff(){
        if (_serverTime){
            return new TimeSpan();
        } else {
            return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("playerTime"));
        }

    }

    public int expLvl {
    	get { return _expLvl; } // Accesor for exp level
    	set { _expLvl = value; } // Mutator for exp level
    }

    public int hungerLvl {
    	get{ return _hungerLvl; } // Accessor for hunger level
    	set{ _hungerLvl = value; } // Mutator for hunger level
    }

    public int slimeLvl {
        get{ return _slimeLvl; } // Accessor for slime level
        set{ _slimeLvl = value; } // Mutator for slime level
    }

}
