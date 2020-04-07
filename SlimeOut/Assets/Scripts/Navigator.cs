using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Navigator : MonoBehaviour
{
    public Button BattleButton;
    public Button CancelButton;
    public Button PVEButton;
    public Button PVPButton;
    public Button EscapeButton;
    public Button ReturnButton;

    public void Start()
    {
        BattleButton.onClick.AddListener(BattleSelectionScene);
        PVEButton.onClick.AddListener(BattleLoadingScene);
        PVPButton.onClick.AddListener(BattleLoadingScene);
        EscapeButton.onClick.AddListener(BattleLostScene);
        ReturnButton.onClick.AddListener(SlimeScene);
        CancelButton.onClick.AddListener(SlimeScene);
    }
    public void SlimeScene() {
		SceneManager.LoadScene("SlimeScene");
	}

	public void HelpScene() {
		SceneManager.LoadScene("HelpScene");
	}

	public void InventoryScene() {
		SceneManager.LoadScene("InventoryScene");
	}

	public void ShopScene() {
		SceneManager.LoadScene("ShopScene");
	}

	public void BattleSelectionScene() {
		SceneManager.LoadScene("BattleSelectionScene");
	}

    public void BattleLoadingScene() {
        SceneManager.LoadScene("BattleLoadingScene");
    }

    public void BattleLostScene() {
        SceneManager.LoadScene("BattleLostScene");
    }

    public void BattleWinScene() {
        SceneManager.LoadScene("BattleWinScene");
    }

    public void LogoutScene() {
        SceneManager.LoadScene("LogoutScene");
    }

	public void Exit() {
		Application.Quit();
	}
}
