using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject Panel;

    // Start is called before the first frame update
    public void OpenPanel(){
        if(Panel != null){
            Panel.SetActive(true);
        }
    }
}
