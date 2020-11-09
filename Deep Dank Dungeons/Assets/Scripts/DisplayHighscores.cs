using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class DisplayHighscores : MonoBehaviour
{
    public Text[] highscoreText;
    Highscores highscoreManager;


    [DllImport("CPlusUtility")]
    public static extern void quickSortInvert(int[] arr, int low, int high);

    //Until the scores are downloaded display "feaching" on all the leaderboard spots, and start the loop of downloading scores
    public void Start()
    {
        for (int i = 0; i < highscoreText.Length; i++)
        {
            highscoreText[i].text = i + 1 + ". Feaching...";
        }
        highscoreManager = GetComponent<Highscores>();

        StartCoroutine("RefreshHighscores");
    }

    //When scores are downloaded and formatted place the first score at the top of the board and work downwards
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

    //Every 30 seconds redownload the scores to keep the leaderboard up to date
    IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highscoreManager.DownloadHighScores();
            yield return new WaitForSeconds(30);
        }
    }

}
