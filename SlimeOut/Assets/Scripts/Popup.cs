using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    public GameObject Panel;

    // Start is called before the first frame update
    public void OpenPanel(){
        // SAVE FUNCTION
        if(Panel != null){
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
        DataBase.instance.SaveDocument();
    }
}
