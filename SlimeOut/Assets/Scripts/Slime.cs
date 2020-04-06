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
    [SerializeField]
    private string _name;
	private bool _serverTime;

   public static Slime instance { get; private set; }

   private void Awake() {
       instance = this;
   }
    // Start is called before the first frame update
    void Start()
    {
        // PlayerPrefs.SetString("playerTime", "04/05/2020 9:00:12");
        updateState(); 

        // Check if slime name is exists
        if(! PlayerPrefs.HasKey("name")){ // No name
            PlayerPrefs.SetString("name", "Jerry"); // Default Name
        } else { // Name exists
            _name = PlayerPrefs.GetString("name");
        }
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

        InvokeRepeating("decreaseHunger", 30f, 30f);

        // TimeSpan timeDiff = getTimeDiff();
        // _hungerLvl  -= (int)(timeDiff.TotalMinutes * 4); // Subtract 4 from hunger every hour
        // if (_hungerLvl  < 0){
        //     _hungerLvl = 0; // Set hunger level to 0 if falls below 0
        // }


        // Use server time if available, else use device time
    	if(_serverTime){
    		updateServer();
    	} else {
            InvokeRepeating("updateDevice", 0f, 30f); // Update time from device every 30s
        }
    }

    void decreaseHunger(){
        // TimeSpan timeDiff = getTimeDiff();
        // _hungerLvl  -= (int)(timeDiff.TotalMinutes * 4); // Subtract 4 from hunger every minute
        // if (_hungerLvl  < 0){
        //     _hungerLvl = 0; // Set hunger level to 0 if falls below 0
        // }

        _hungerLvl  -= (int)(2); // Subtract 1 from hunger every minute
        if (_hungerLvl  < 0){
            _hungerLvl = 0; // Set hunger level to 0 if falls below 0
        }
        PlayerPrefs.SetInt("_hungerLvl", _hungerLvl);
    }

    void updateServer(){
        // If have server after
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
    	set { // Mutator for exp level
            _expLvl = value;
            PlayerPrefs.SetInt("_expLvl", _expLvl);
        } 
    }

    public int hungerLvl {
    	get{ return _hungerLvl; } // Accessor for hunger level
    	set{ // Mutator for hunger level
            _hungerLvl = value;
            PlayerPrefs.SetInt("_hungerLvl", _hungerLvl);
        } 
    }

    public string name {
        get{ return _name; } // Accessor for slime name
        set{ // Mutator for slime name
            _name = value;
            PlayerPrefs.SetString("name", "Jerry");
        } 
    }

    public int slimeLvl {
        get{ return _slimeLvl; } // Accessor for slime level
        set{ // Mutator for slime level
            _slimeLvl = value;
            PlayerPrefs.SetInt("_slimeLvl", _slimeLvl);

        } 
    }

    // Add exp to exp level
    public void updateExpLvl(int exp){
        _expLvl += exp;

        PlayerPrefs.SetInt("_expLvl", _expLvl);

        if (_expLvl >= 100){
            _expLvl = 0;
            _slimeLvl += 1;
            PlayerPrefs.SetInt("_slimeLvl", _slimeLvl);
        }
    }

}
