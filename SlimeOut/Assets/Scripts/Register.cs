using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using MongoDB.Driver;
using MongoDB.Bson;

namespace universal
{
    /// <summary>
    /// Register checks if username already exists in the database and if not
    /// inserts the username and other info in the database
    /// </summary>
    public class Register : MonoBehaviour
    {
        public GameObject username;
        public GameObject email;
        public GameObject password;
        public GameObject retype_password;
        public GameObject slimename;
        public Dropdown slime_color;

        private static string Username;
        private string Email;
        private string Password;
        private string Retype_password;
        private string Slimename;
        private string Slime_color;

        List<string> colors = new List<string>()
                                            { "Please select slime color",
                                              "Red",
                                              "Blue",
                                              "Green"
                                            };

        // private string form;
        private bool email_valid = false;

        private string[] Characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                    "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "_", "-" };





        // Start is called before the first frame update
        /// <summary>
        /// populates the slime colors and makes a database and fills in user_info collection
        /// </summary>
        void Start()
        {
            PopulateList();
        }
        public void Register_button()
        {
            bool UN = false;
            bool EM = false;
            bool PW = false;
            bool RPW = false;

            if (Username != "")
            {
                if (!DataBase.instance.UserExists(Username))
                {
                    UN = true;
                }
                else
                {
                    Debug.LogWarning("Username Taken here");
                }
            }
            else
            {
                Debug.LogWarning("Username field is empty");
            }

            if (Email != "")
            {
                email_validation();
                if (email_valid)
                {
                    if (Email.Contains("@"))
                    {
                        if (Email.Contains("."))
                        {
                            EM = true;
                        }
                        else
                        {
                            Debug.LogWarning("Email is incorrect");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Email is incorrect");
                    }
                }
                else
                {
                    Debug.LogWarning("Email is incorrect");
                }
            }
            else
            {
                Debug.LogWarning("Email field empty");
            }

            if (Password != "")
            {
                if (Password.Length > 5)
                {
                    PW = true;
                }
                else
                {
                    Debug.LogWarning("Password must be atleast 6 characters");
                }

            }
            else
            {
                Debug.LogWarning("Password field is empty");
            }

            if (Retype_password != "")
            {
                if (Retype_password == Password)
                {
                    RPW = true;
                }
                else
                {
                    Debug.LogWarning("Passwords dont match");
                }
            }
            else
            {
                Debug.LogWarning("Re-type password field is empty");
            }

            if (UN == true && EM == true && RPW == true && PW == true)
            {
                DataBase.instance.Register(Username, Email, Password, Slimename, Slime_color);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (username.GetComponent<InputField>().isFocused)
                {
                    email.GetComponent<InputField>().Select();
                }
                if (email.GetComponent<InputField>().isFocused)
                {
                    password.GetComponent<InputField>().Select();
                }
                if (password.GetComponent<InputField>().isFocused)
                {
                    retype_password.GetComponent<InputField>().Select();
                }
                if (retype_password.GetComponent<InputField>().isFocused)
                {
                    slimename.GetComponent<InputField>().Select();
                }
                if (slimename.GetComponent<InputField>().isFocused)
                {
                    slime_color.GetComponent<Dropdown>().Select();
                }

            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (Username != "" && Email != "" && Retype_password != "")
                {
                    Register_button();
                }
            }

            Username = username.GetComponent<InputField>().text;
            Email = email.GetComponent<InputField>().text;
            Password = password.GetComponent<InputField>().text;
            Retype_password = retype_password.GetComponent<InputField>().text;
            Slimename = slimename.GetComponent<InputField>().text;
        }

        void email_validation()
        {
            bool SW = false;
            bool EW = false;

            for (int i = 0; i < Characters.Length; i++)
            {
                if (Email.StartsWith(Characters[i]))
                {
                    SW = true;
                }
            }

            for (int i = 0; i < Characters.Length; i++)
            {
                if (Email.EndsWith(Characters[i]))
                {
                    EW = true;
                }
            }
            if (SW && EW)
            {
                email_valid = true;
            }
            else
            {
                email_valid = false;
            }
        }

        void PopulateList()
        {
            slime_color.AddOptions(colors);
        }

        public void Dropdown_IndexChanged(int i)
        {
            Slime_color = colors[i];
        }
    }
}



