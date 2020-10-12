﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighscores : MonoBehaviour
{
    public Text[] highscoreText;
    Highscores highscoreManager;

    public void Start()
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". Feaching...";

        }
        highscoreManager = GetComponent<Highscores>();

        StartCoroutine("RefreshHighscores");
    }
    
    public void OnHighscoresDownloaded(Highscore[] highscoreList)
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". ";
            if(highscoreList.Length > i)
            {
                highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
            }
        }
    }
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighScores();
            yield return new WaitForSeconds(30);
        }
    }

}
