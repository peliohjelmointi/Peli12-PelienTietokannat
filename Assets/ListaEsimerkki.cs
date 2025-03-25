using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ListaEsimerkki : MonoBehaviour
{
    List<string> simpsons = new List<string>();

    //tuple (string,int) //voi sis‰lt‰‰ x m‰‰r‰n muuttujia
    List<(string, int)> highscoreList = new List<(string,int)>();

    private void Start()
    {
        simpsons.Add("Homer");
        simpsons.Add("Marge");
        simpsons.Remove("Homer");

        string[] nimet = { "Herra Huu", "Aku Ankka", "Mikki" };
        List <string> names =   nimet.ToList();

        simpsons.Clear(); //tyhj‰‰ listan (sama kuin alussa)
        highscoreList.Add(   ("Homer", 100)     );
                
                //(string,int) rivi //k‰y myˆs, yleens‰ var n‰iss‰ 
        foreach ( var rivi in highscoreList)
        {
            print(rivi);
        }

    }
}
