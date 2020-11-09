using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class SortHighscores : MonoBehaviour
{
    Highscores highscoreManager;
    DisplayHighscores highscoresDisplay;
    GameObject highscoreScript;

    public bool invertSort = false;
    public bool randomize = false;
    private void Start()
    {
        highscoreManager = GameObject.Find("Highscores").GetComponent<Highscores>();
        highscoresDisplay = GameObject.Find("Highscores").GetComponent<DisplayHighscores>();
    }

    //Turn the randomize bool to true and refresh the leaderboard
    public void randomizeScores()
    {
        highscoreManager.randomize = true;
        highscoresDisplay.StopCoroutine("RefreshHighscores");
        highscoresDisplay.StartCoroutine("RefreshHighscores");
    }

    //Turn the invert sort bool on and refresh the leaderboard
    public void sortHighscores()
    {
        if(invertSort == true)
        {
            invertSort = false;
            highscoresDisplay.StopCoroutine("RefreshHighscores");
            highscoresDisplay.StartCoroutine("RefreshHighscores");
        }
        else
        {
            invertSort = true;
            highscoresDisplay.StopCoroutine("RefreshHighscores");
            highscoresDisplay.StartCoroutine("RefreshHighscores");
        }
    }

    
}
