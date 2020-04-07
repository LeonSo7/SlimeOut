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
    private int _colour; //0G, 1B, 2R (Slime colour)
    [SerializeField]
    private string _name;
	// private bool _serverTime;

   public static Slime instance { get; private set; }

   private void Awake() {
       if (instance == null) {
            DontDestroyOnLoad (gameObject);
            instance = this;
        } 
        else if (instance != this) {
            Destroy(gameObject);
        }
   }
   
    void Start()
    {
        updateState(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateState(){
        // Check if slime name exists
        if(! PlayerPrefs.HasKey("name")){ // No name
            _name = "Jerry";
            PlayerPrefs.SetString("name", _name); // Default Name
        } else { // Name exists
            _name = PlayerPrefs.GetString("name");
        }

        // Check if slime colour exists
        if(! PlayerPrefs.HasKey("colour")){ // No colour
            _colour = 0;
            PlayerPrefs.SetInt("colour", _colour); // Default colour
        } else { // Name exists
            _colour = PlayerPrefs.GetInt("colour");
        }

        // For testing - set colour
        // _colour = 0;
        // PlayerPrefs.SetInt("colour", _colour);

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

        // For testing - set hunger
        // _hungerLvl = 2;
        //     PlayerPrefs.SetInt("_hungerLvl", _hungerLvl);

        if (PlayerPrefs.HasKey("_slimeLvl")){
            _slimeLvl = PlayerPrefs.GetInt("_slimeLvl");
        } else { // Default state - slime level
            _slimeLvl = 1;
            PlayerPrefs.SetInt("_slimeLvl", _slimeLvl);
        }

        InvokeRepeating("decreaseHunger", 30f, 30f);
    }

    void decreaseHunger(){

        _hungerLvl  -= (int)(2); // Subtract 1 from hunger every minute
        if (_hungerLvl  < 0){
            _hungerLvl = 0; // Set hunger level to 0 if falls below 0
        }
        PlayerPrefs.SetInt("_hungerLvl", _hungerLvl);
    }

    // TimeSpan getTimeDiff(){
    //     if (_serverTime){
    //         return new TimeSpan();
    //     } else {
    //         return DateTime.Now - Convert.ToDateTime(PlayerPrefs.GetString("playerTime"));
    //     }

    // }

    public int expLvl {
    	get { return _expLvl; } // Accesor for exp level
    	set { // Mutator for exp level
            _expLvl = value;
            if (_expLvl >= 100){
                _expLvl = 0;
                _slimeLvl += 1;
                PlayerPrefs.SetInt("_slimeLvl", _slimeLvl);
            }
            if (_expLvl  < 0){
                _expLvl = 0; // Minimum exp level
            }
        } 
    }

    public int hungerLvl {
    	get{ return _hungerLvl; } // Accessor for hunger level
    	set{ // Mutator for hunger level
            _hungerLvl = value;
            if (_hungerLvl >= 100){
                _hungerLvl = 100;
            }
            if (_hungerLvl  < 0){
                _hungerLvl = 0; // Minimum hunger level
            }
            PlayerPrefs.SetInt("_hungerLvl", _hungerLvl);
        } 
    }

    public string name {
        get{ return _name; } // Accessor for slime name
        set{ // Mutator for slime name
            _name = value;
            PlayerPrefs.SetString("name", _name); // Save name to PlayerPrefs
        } 
    }

    public int slimeLvl {
        get{ return _slimeLvl; } // Accessor for slime level
        set{ // Mutator for slime level
            _slimeLvl = value;
            if (_slimeLvl  < 1){
                _slimeLvl = 1; // Minimum slime level
            }
            PlayerPrefs.SetInt("_slimeLvl", _slimeLvl);

        } 
    }

    // Add exp to exp level
    public void updateExpLvl(int exp){
        _expLvl = exp;

        PlayerPrefs.SetInt("_expLvl", _expLvl);

        if (_expLvl >= 100){
            _expLvl = 0;
            _slimeLvl += 1;
            PlayerPrefs.SetInt("_slimeLvl", _slimeLvl);
        }
    }

    public int colour {
        get{ return _colour; } // Accessor for colour
        set{ // Mutator for colour
            _colour = value;
            PlayerPrefs.SetInt("colour", _colour);
        } 
    }

}
