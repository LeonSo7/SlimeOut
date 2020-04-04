using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.UI;

using MongoDB.Bson;
using MongoDB.Driver;


public class Register : MonoBehaviour
{
    // mongo
    public class Order
    {
        public enum STATE : byte
        {
            NEW = 0,
            CANCELLED = 1,
            COMPLETE = 2
        }
        public ObjectId Id { get; set; }
        public string U { get; set; }
        public string E { get; set; }
        public string P { get; set; }
    }

    private MongoClient client;
    private MongoServer server;
    private MongoDatabase db;
    private MongoCollection<Order> orders;
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

    private string form;
    private bool email_valid = false;

    private string[] Characters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                                    "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
                                    "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "_", "-" };





    // Start is called before the first frame update
    void Start()
    {
        PopulateList();
        //mongo
        client = new MongoClient(new MongoUrl("mongodb://localhost"));
        server = client.GetServer();
        server.Connect();
        db = server.GetDatabase("local");
        orders = db.GetCollection<Order>("orders");
        //
    }

    private void InsertDocument()
    {
        var order = new Order
        {
            U = "hello",
            P = "12345",
            E = "hello@gmail.com"
        };
        orders.Insert(order);
        Debug.Log("It seems that its adding data in the database");

        // var temp = orders.Find(U == "hello").ToList();
    }

    public void Register_button()
    {
        bool UN = false;
        bool EM = false;
        bool PW = false;
        bool RPW = false;

        if(Username != "")
        {
            if (!System.IO.File.Exists(Username + ".txt"))
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

        if(Email != "")
        {
            email_validation();
            if (email_valid)
            {
                if (Email.Contains("@")){
                    if (Email.Contains("."))
                    {
                        EM = true;
                    }
                    else{
                        Debug.LogWarning("Email is incorrect");
                    }
                }
                else{
                    Debug.LogWarning("Email is incorrect");
                }
            }
            else{
                Debug.LogWarning("Email is incorrect");
            }
        }else{
            Debug.LogWarning("Email field empty");
        }

        if (Password != "")
        {
            if(Password.Length > 5)
            {
                PW = true;
            }
            else
            {
                Debug.LogWarning("Password must be atleast 6 characters");
            }

        } else{
            Debug.LogWarning("Password field is empty");
        }

        if (Retype_password != "")
        {
            if(Retype_password == Password)
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

        if (UN == true && EM == true && RPW == true)
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
            form = (Username + Environment.NewLine + Email + Environment.NewLine + Password
                    + Environment.NewLine + Slimename + Environment.NewLine + Slime_color);
            System.IO.File.WriteAllText(Username + ".txt", form);

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
        if(SW && EW)
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


}
