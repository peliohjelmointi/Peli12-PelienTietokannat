using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    string dbName;

    int x = 5;
    string s = "S";

 
    private void Awake()
    {     
        dbName = "URI=file:" + 
            Application.persistentDataPath + "/highscores.db";
        CreateDbAndTable("scores");
        AddHighscore("scores", "PLAYER X", 1000);
    }

    void CreateDbAndTable(string tableName)
    {
        #region regex
        //tarkistetaan, ett� taulun nimess� on vain kirjaimia tai numeroita tai _
        if (!Regex.IsMatch(tableName, @"^[a-zA-Z0-9_]+$"))
        {   //antaa virheilmoituksen unityss� 
            throw new ArgumentException("Invalid table name");
        }
        #endregion
        string createTableQuery = 
            $"CREATE TABLE IF NOT EXISTS {tableName} (name TEXT, score INTEGER);";        
        ExecuteSQL(createTableQuery);
    }

    void AddHighscore(string tableName, string playerName, int playerScore)
    {
        #region regex
        //tarkistetaan, ett� taulun nimess� on vain kirjaimia tai numeroita tai _
        if (!Regex.IsMatch(tableName, @"^[a-zA-Z0-9_]+$"))
        {   //antaa virheilmoituksen unityss� 
            throw new ArgumentException("Invalid table name");
        }
        #endregion
        string insertQuery = $"INSERT INTO {tableName} (name, score) VALUES (@name,@score);";
        ExecuteSQL(insertQuery, ("@name", playerName), ("@score", playerScore));
    }

    void ExecuteSQL(string sql, params (string, object)[] parameters)  //0 tai useampi parametri
    {
        using (var connection = new SqliteConnection(dbName))
        {
            connection.Open(); //koittaa avata dbNamessa m��ritetyn db-tiedoston
            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;

                //k�yd��n l�pi kaikki tulleet tuple-parametrit (jos niit� on)
                    //paramName = @name , paramValue = @score
                foreach (var (paramName, paramValue) in parameters)
                {
                    command.Parameters.AddWithValue(paramName, paramValue);
                }
                command.ExecuteNonQuery();
            }
        }
    }

}
