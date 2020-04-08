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

   public static Slime instance { get; set; }

   private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad (gameObject);
            instance = this;
            DataBase db = DataBase.instance;
            hungerLvl = db.hunger;
            // Uncomment to force and set hunger
            // hungerLvl = 2;
            expLvl = db.exp;
            slimeLvl = db.lvl;
            name = db.name;
            Inventory.instance.balance = db.balance;
            Inventory.instance.setFromStringArray(db.items);
            switch (db.col) {
                default:
                case "Green": colour = 0; break;
                case "Blue": colour = 1; break;
                case "Red": colour = 2; break;
            }
            updateState(); 
        } 
        else if (instance != this) {
            Destroy(gameObject);
        }
        
   }

    void updateState(){
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
