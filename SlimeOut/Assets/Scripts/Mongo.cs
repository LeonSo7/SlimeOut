using MongoDB.Driver;
using UnityEngine;

public class Mongo
{
    private const string MONGO_URI = "";
    private const string DATABASE_NAME = "";

    private MongoClient client;
    private MongoServer server;
    private MongoDatabase db;

    private void Init()
    {
        client = new MongoClient(MONGO_URI);
        server = client.GetServer();
        db = server.GetDatabase(DATABASE_NAME);

        // This is where we would initialize collections

        Debug.Log("Databse has been initialized");
    }

    public void Shutdown()
    {
        client = null;
        server.Shutdown();
        db = null;
    }
}
