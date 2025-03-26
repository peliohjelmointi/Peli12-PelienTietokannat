using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic; //List

public class MongoTest : MonoBehaviour
{                                               //connection string, löytyy mongon clusters-valikosta connect-napin takaa
    MongoClient mongoClient = new MongoClient("mongodb+srv://julgubbe:julgubbe@cluster0.37ex2rd.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");

    IMongoDatabase db;
    IMongoCollection<BsonDocument> collection; //vrt sql:n taulu

    private void Start()
    {
        db = mongoClient.GetDatabase("PisteetDB");
        collection = db.GetCollection<BsonDocument>("PisteetCollection");

        //PrintAllDatabases();     
        AddHighscore("Kalle", 50);
    }

    async void AddHighscore(string name, int score)
    {
        BsonDocument document = new BsonDocument()
            .Add("name", name)
            .Add("score", score);

        //BsonDocument doc = new BsonDocument().Add("year", 1963);        
        await collection.InsertOneAsync(document);
        //await collection.InsertOneAsync(doc);
    }

    void PrintAllDatabases()
    {
        try
        {
            //listataan kaikkien tietokantojen nimet
            var dbList = mongoClient.ListDatabaseNames().ToList();
            foreach (var row in dbList)
            {
                print(row);
            }

        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

}
