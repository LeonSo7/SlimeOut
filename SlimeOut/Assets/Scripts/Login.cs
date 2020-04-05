using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MongoDB.Driver.Builders;

using universal;

public class Login : MonoBehaviour
{
    public GameObject username;
    public GameObject password;

    private string Username;
    private string Password;
    private string Lines;
    private string Decrypted_password;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Login_button()
    {
        bool UN = false;
        bool PW = false;

        if(Username != "")
        {
            if(Register.UserExists(Username) != null)
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
            if (Register.UserExists(Username) != null)
            {
                
                int i = 1;
                Lines = Register.UserExists(Username).P;
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
