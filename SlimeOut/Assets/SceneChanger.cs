using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void GoToShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
    public void GoToInventory()
    {
        SceneManager.LoadScene("InventoryScene");
    }
    public void GoToSlime()
    {
        SceneManager.LoadScene("SlimeScene");
    }
}
