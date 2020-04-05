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

        if(Username != "")
        {
            if(Register.UserData(Username) != null)
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

        if(Password != "")
        {
            if (Register.UserData(Username) != null)
            {
                
                int i = 1;
                Lines = Register.UserData(Username).O_password;
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
