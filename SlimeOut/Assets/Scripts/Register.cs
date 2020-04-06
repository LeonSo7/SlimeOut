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
        // mongo
        public MongoClient client;
        public static  IMongoDatabase db;
        public static IMongoCollection<Order> user_info;

        //

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

            // client = new MongoClient(new MongoUrl("mongodb://:Atal123@Atal@cluster0-1i8se.mongodb.net/test?retryWrites=false&w=majority"));
            client = new MongoClient("mongodb+srv://tiwarimkt:Atal123Atal@cluster0-1i8se.mongodb.net/gameDB?retryWrites=true&w=majority");
            db = client.GetDatabase("gameDB");
            user_info = db.GetCollection<Order>("user_info");
            //
            
        }

        public void InsertDocument()
        {
            var info = new Order
            {
                O_username = Username,
                O_email = Email,
                O_password = Password,
                O_slimename = Slimename,
                O_slime_color = Slime_color,
                O_balance = 1000,
                O_health = 100,
                O_slime_level = 0,
                O_hunger_level = 0,
                O_exp_level = 0,
                O_item_strings = new string[0],
            };
            user_info.InsertOne(info);
            Debug.Log("seems like its working");

            // Debug.Log("It seems that its adding data in the database");
        }
        public static void SaveInventory() {
            Inventory inv = Inventory.instance;
            string[] items = new string[inv.items.Count];

            int j = 0;
            foreach(var i in inv.items) items[j++] = i.ToString();

            var info = new InvSchema
            {
                O_username = Username,
                O_items = items,
                O_balance = inv.balance,
            };
        }

        public void Register_button()
        {
            bool UN = false;
            bool EM = false;
            bool PW = false;
            bool RPW = false;

            if (Username != "")
            {
                if (!UserExists(Username))
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

        public void Dropdown_IndexChanged(int i)
        {
            Slime_color = colors[i];
        }

        #region
        /// <summary>
        /// to list all documents in user_info collection in gameDB database
        /// </summary>
        public static List<Order> UserData( )
            {
                return user_info.Find(new BsonDocument()).ToList();
            }
        #endregion

        #region
        /// <summary>
        /// to check if user already exists in the database
        /// </summary>
        public static bool UserExists(string uname)
        {
            var recs = UserData();
            foreach (var rec in recs)
            {
                if (rec.O_username.Equals(uname))
                {
                     // Debug.Log(rec.O_username);
                    return true;
                }
            }
            return false;
        }
        #endregion

        public static string GetPassword(string uname)
        {
            var recs = UserData();
            foreach (var rec in recs)
            {
                if (rec.O_username.Equals(uname))
                {
                    return rec.O_password;
                } 
            }
            return null;
        }
    }
}



