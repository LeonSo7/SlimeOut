using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        updateState(); 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void updateState(){
    	if (PlayerPrefs.HasKey("_expLvl")){
    		_expLvl = PlayerPrefs.GetInt("_expLvl");
    	} else {
    		_expLvl = 0;
    		PlayerPrefs.SetInt("_expLvl", _expLvl);
    	}

    	if (PlayerPrefs.HasKey("_hungerLvl")){
    		_hungerLvl = PlayerPrefs.GetInt("_hungerLvl");
    	} else {
    		_hungerLvl = 100;
    		PlayerPrefs.SetInt("_hungerLvl", _hungerLvl);
    	}

        if (PlayerPrefs.HasKey("_slimeLvl")){
            _expLvl = PlayerPrefs.GetInt("_slimeLvl");
        } else {
            _expLvl = 1;
            PlayerPrefs.SetInt("_slimeLvl", _expLvl);
        }

    	if(_serverTime){
    		updateServer();
    	}
    }

    void updateServer(){

    }

    public int expLvl {
    	get { return _expLvl; } //Accesor for exp level
    	set { _expLvl = value; } //Mutator for exp level
    }

    public int hungerLvl {
    	get{ return _hungerLvl; } //Accessor for hunger level
    	set{ _hungerLvl = value; } //Mutator for hunger level
    }

    public int slimeLvl {
        get{ return _slimeLvl; } //Accessor for slime level
        set{ _slimeLvl = value; } //Mutator for slime level
    }
}
}
