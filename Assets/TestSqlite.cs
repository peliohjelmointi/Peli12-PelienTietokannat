using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

public class TestSqlite : MonoBehaviour
{
    string dbName;
    //List<(string name, int score)> highscoreList = new List<(string, int)>();
   
    private void Start()
    {
                                //Assets-kansio Assets/uusi_tietokanta.db
        dbName = "URI=file:" + Application.dataPath + "/uusi_tietokanta.db";

                        //Application.persistentDataPath = k‰ytt‰j‰n profiiliin
        CreateTable();
        //AddHighscore("KALLE", 100);
        //GetHighscores();

        //LINQ
        //highscoreList = highscoreList.OrderByDescending(x => x.score).ToList();

        //Sort & CompareTo
        //highscoreList.Sort((a, b) => b.score.CompareTo(a.score));

        //foreach ( (string name, int score)scoreRow in highscoreList)
        //foreach (var scoreRow in highscoreList)
        //{
        //    print(scoreRow);
        //}

            //Assets-kansio
        // print(Application.dataPath); 

        // K‰ytt‰j‰n profiilissa oleva kansio
        // Suositeltu tapa k‰ytt‰‰ t‰t‰
        // print(Application.persistentDataPath);

    }

    void CreateTable()
    {               

        //using var connection = new SqliteConnection(dbName); //uusi tapa
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open(); 
            
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE IF NOT EXISTS highscores(" +
                      " id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT, score INT)";

                command.ExecuteNonQuery(); // INSERT, UPDATE, DELETE, CREATE, DROP
                // SELECT-lauseissa "tulisi" k‰ytt‰‰ ExecuteReader()
                // ExecuteScalar() tulisi k‰ytt‰‰ jos haetaan yksitt‰ist‰ arvoa
            }
           
        }
            

    } // CreateTable("highscores") //parametriksi luotava taulu

    // lis‰‰ yhden highscoren highscores-tauluun
    void AddHighscore(string tableName, string playerName, int score)
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using(var command = connection.CreateCommand())
            {

                if (!Regex.IsMatch(tableName, @"^[a-zA-Z0-9_]+$"))
                {
                    throw new ArgumentException("Invalid table name");
                }

                command.CommandText = $"INSERT INTO {tableName} (name, score) VALUES (@nimi, @pisteet)";
                command.Parameters.AddWithValue("@nimi", playerName);
                command.Parameters.AddWithValue("@pisteet", score);
                command.ExecuteNonQuery();

            }
            connection.Close();
        }
    }
    
    //hakee kaikki highscoret, MUOKKAA haluamaksesi
    void GetHighscores()
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT * FROM highscores";
                using (IDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read()) //k‰yd‰‰n l‰pi rivi rivilt‰ kursorin rivej‰
                    {                        
                        string name = reader["name"].ToString();
                        int score = Convert.ToInt32(reader["score"]);
                        //highscoreList.Add((name, score));
 //highscoreList.Add((reader["name"].ToString(), Convert.ToInt32(reader["score"])));
                    }                   
                }              
            }
           
        }
    }
    // LUO LUOKKAAN HIGHSCORE-LISTA (LIST)
    // JA LISƒƒ JOKAINEN RIVI LISTAAN
    // (JOKAINEN RIVI = NIMI, SCORE)
    // TULOSTA LISTA (KOKEILE SORTATA)

}
