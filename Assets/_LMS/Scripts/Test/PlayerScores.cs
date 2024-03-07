using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using static System.Net.Mime.MediaTypeNames;

public class PlayerScores : MonoBehaviour
{
    public static int playerScore;
    public static string playerName;
    public static int playerWeight;

    readonly string link = "https://testapi-81614-default-rtdb.asia-southeast1.firebasedatabase.app/";

    public void OnTest()
    {
        playerName = "PETERS";
        playerScore = 789789;
        PostToDatabase();
    }

    private void PostToDatabase()
    {
        User user = new User();
        RestClient.Put(link + playerName + ".json", user);
    }
}