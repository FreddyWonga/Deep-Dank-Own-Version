using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class SaveFinalScore : MonoBehaviour
{
    private Score scores;
    public string theName;
    public string ScoreString;
    public GameObject inputField;
    public string finalScore;
    public Score score;
    public StatTracker StatTracker;
    public GameObject SaveButton;
    public Highscores highscores;
    public LinkedList<string> usernames;

    public void SaveScoreName()
    {
        int scores = StatTracker.Instance.currentScore;
        theName = inputField.GetComponent<Text>().text;
        highscores = GetComponent<Highscores>();
        Highscores.AddNewHighscore(theName, scores);
        usernames.AddFirst(theName);
    }
}
