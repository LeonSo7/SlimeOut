using System.Collections.Generic;
using UnityEngine;

using MongoDB.Driver;
using MongoDB.Bson;

namespace universal
{
    /// <summary>
    /// Register checks if username already exists in the database and if not
    /// inserts the username and other info in the database
    /// </summary>
    public class DataBase : MonoBehaviour
    {
        // mongo
        public MongoClient client;
        public static  IMongoDatabase db;
        public static IMongoCollection<Order> user_info;

        private static string Username;
        private string Email;
        private string Password;
        private string Retype_password;
        private string Slimename;
        private string Slime_color;
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
        public void SaveDocument() {
            var info = new Order
            {
                O_username = Username,
                O_email = Email,
                O_password = Password,
                O_slimename = Slimename,
                O_slime_color = Slime_color,
                O_balance = Inventory.instance.balance,
                O_health = 100,
                O_slime_level = Slime.instance.slimeLvl,
                O_hunger_level = Slime.instance.hungerLvl,
                O_exp_level = Slime.instance.slimeLvl,
                O_item_strings = Inventory.instance.ToStringArray(),
            };
            var filter = Builders<Order>.Filter.Eq("O_username", Username);
            user_info.FindOneAndReplace(filter, info);
            Debug.Log("seems like its working");
        }
        public void Register(string uname, string em, string pword, string sname, string scol)
        {
            bool Clear = true;
            Username = uname;
            Email = em;
            Password = pword;
            Slimename = sname;
            Slime_color = scol;
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
}