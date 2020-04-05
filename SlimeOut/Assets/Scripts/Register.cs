using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace universal
{
    /// <summary>
    /// Register checks if username already exists in the database and if not
    /// inserts the username and other info in the database
    /// </summary>
    public class Register : MonoBehaviour
    {
        // mongo
        public MongoClient client;
        public MongoServer server;
        public MongoDatabase db;
        public static MongoCollection<Order> user_info;
        //

        public GameObject username;
        public GameObject email;
        public GameObject password;
        public GameObject retype_password;
        public GameObject slimename;
        public Dropdown slime_color;

        private string Username;
        private string Email;
        private string Password;
        private string Retype_password;
        private string Slimename;
        private string Slime_color;

        List<string> colors = new List<string>()
                                            { "Please select slime color",
                                              "Green",
                                              "Purple",
                                              "Red"
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
            
            client = new MongoClient(new MongoUrl("mongodb://localhost"));
            server = client.GetServer();
            server.Connect();
            db = server.GetDatabase("local");
            user_info = db.GetCollection<Order>("user_info");
        }

        public void InsertDocument()
        {
            var info = new Order
            {
                U = Username,
                P = Password,
                E = Email,
                S = Slimename,
                SC = Slime_color
            };
            user_info.Insert(info);

            // Debug.Log("It seems that its adding data in the database");
        }

        public void Register_button()
        {
            bool UN = false;
            bool EM = false;
            bool PW = false;
            bool RPW = false;

            if (Username != "")
            {
                if (UserData(Username) == null)
                {
                    UN = true;
                }
                else
                {
                    Debug.LogWarning("Username Taken");
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
                bool Clear = true;
                int i = 1;
                foreach (char c in Password)
                {
                    if (Clear)
                    {
                        Password = "";
                        Clear = false;
                    }
                    i++;
                    char Encrypted = (char)(c * i);
                    Password += Encrypted.ToString();
                }
                // form = (Username + Environment.NewLine + Email + Environment.NewLine + Password
                //        + Environment.NewLine + Slimename + Environment.NewLine + Slime_color);
                // System.IO.File.WriteAllText(Username + ".txt", form);

                InsertDocument();

                username.GetComponent<InputField>().text = "";
                email.GetComponent<InputField>().text = "";
                password.GetComponent<InputField>().text = "";
                retype_password.GetComponent<InputField>().text = "";
                slimename.GetComponent<InputField>().text = "";
                slime_color.ClearOptions();

                print("Registration Complete");
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

        public void Dropdown_IndexChanged(int index)
        {
            Slime_color = colors[index];
        }

        #region
        /// <summary>
        /// to check if the user already exits in the database
        /// </summary>
        public static Order UserData(string u_name)
        {
            var query = Query<Order>.EQ(u => u.U, u_name);
            return user_info.FindOne(query);
        }
        #endregion

    }

}



