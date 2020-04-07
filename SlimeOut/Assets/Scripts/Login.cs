using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

using universal;

/// <summary>
/// checks if user exists in the database, if does, ten transitions to slime scene
/// </summary>
public class Login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;

    private string Username;
    private string Password;
    private string Lines;
    private string Decrypted_password;

    /// <summary>
    /// event handler when login button is clicked
    /// </summary>
    public void Login_button()
    {
        bool UN = false;
        bool PW = false;

        if (Username != "")
        {
            if (DataBase.instance.UserExists(Username))
            {
                UN = true;
                // Lines = System.IO.File.ReadAllLines(Username + ".txt");
            }
            else
            {
                Debug.LogWarning("Invalid username");
            }
        }
        else
        {
            Debug.LogWarning("Username field empty");
        }

        if (Password != "")
        {
            if (DataBase.instance.UserExists(Username))
            {

                int i = 1;
                if (DataBase.instance.GetPassword(Username) != null)
                {
                    Lines = DataBase.instance.GetPassword(Username);
                }
                else
                {
                    Debug.Log("Password null");
                }

                foreach (char c in Lines)
                {
                    i++;
                    char Decrypted = (char)(c / i);
                    Decrypted_password += Decrypted.ToString();
                }
                if (Password == Decrypted_password)
                {
                    PW = true;
                }
                else
                {
                    Debug.LogWarning("Wrong password");
                }
            }
            else
            {
                Debug.LogWarning("Password is invalid");
            }
        }
        else
        {
            Debug.LogWarning("Password field is empty");
        }

        if (UN && PW)
        {
            username.GetComponent<InputField>().text = ""; 
            password.GetComponent<InputField>().text = ""; 
            print("Login successful");
            DataBase.instance.LoadUserData(Username);
            SceneManager.LoadScene("SlimeScene");
        }
    }

    // Update is called once per frame
    /// <summary>
    /// gets the username and password from UI and and does processing when button is pressed
    /// </summary>
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (username.GetComponent<InputField>().isFocused)
            {
                password.GetComponent<InputField>().Select();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Username != "" && Password != "")
            {
                Login_button();
            }
        }

        Username = username.GetComponent<InputField>().text;
        Password = password.GetComponent<InputField>().text;
    }

}
