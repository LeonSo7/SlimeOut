using System.Collections.Generic;
using UnityEngine;

using MongoDB.Driver;
using MongoDB.Bson;

public class DataBase : MonoBehaviour
{
    // mongo
    public MongoClient client;
    public static  IMongoDatabase db;
    public static IMongoCollection<Order> user_info;

    private string Username;
    private string Email;
    private string Password;
    public string Slimename;
    public string col;
    public string name;
    public int lvl;
    public int exp;
    public int hunger;
    public int balance;
    public string[] items;
    public static DataBase instance { get; private set; }

    private void Awake() {
        if (instance == null) {
            DontDestroyOnLoad (gameObject);
            instance = this;
        } 
        else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start()
    {
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
            O_slime_color = col,
            O_balance = 50,
            O_health = 100,
            O_slime_level = 0,
            O_hunger_level = 100,
            O_exp_level = 0,
            O_item_strings = new string[0],
        };
        user_info.InsertOne(info);
        Debug.Log("seems like its working");

        // Debug.Log("It seems that its adding data in the database");
    }
    
    public void SaveDocument() {
        var filter = Builders<Order>.Filter.Eq("O_username", Username);
        var update = Builders<Order>.Update.Set("O_slimename", Slime.instance.name)
            .Set("O_slime_color", col)
            .Set("O_balance", Inventory.instance.balance)
            .Set("O_slime_level", Slime.instance.slimeLvl)
            .Set("O_hunger_level", Slime.instance.hungerLvl)
            .Set("O_exp_level", Slime.instance.expLvl)
            .Set("O_item_strings", Inventory.instance.ToStringArray());
        user_info.FindOneAndUpdate(filter, update);
        Debug.Log("Doc updated?");
    }
    public void Register(string uname, string em, string pword, string sname, string scol)
    {
        bool Clear = true;
        Username = uname;
        Email = em;
        Password = pword;
        Slimename = sname;
        col = scol;
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
        print("Registration Complete");
    }

    #region
    /// <summary>
    /// to list all documents in user_info collection in gameDB database
    /// </summary>
    public List<Order> UserData( )
    {
        return user_info.Find(new BsonDocument()).ToList();
    }
    #endregion
    public void LoadUserData(string uname) {
        Username = uname;
        var recs = instance.UserData();
        foreach (var rec in recs)
        {
            if (rec.O_username.Equals(uname))
            {
                Slimename = rec.O_slimename;
                col = rec.O_slime_color;
                lvl = rec.O_slime_level;
                exp = rec.O_exp_level;
                hunger = rec.O_hunger_level;
                balance = rec.O_balance;
                items = rec.O_item_strings;
                name = rec.O_slimename;
            }
        }
    }
    #region
    /// <summary>
    /// to check if user already exists in the database
    /// </summary>
    public bool UserExists(string uname)
    {
        var recs = instance.UserData();
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

    public string GetPassword(string uname)
    {
        var recs = instance.UserData();
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
