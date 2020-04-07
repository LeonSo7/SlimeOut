using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        load();
    }

    IEnumerator load()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("InBattleScene");
    }
    // Update is called once per frame
}
