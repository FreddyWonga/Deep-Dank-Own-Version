using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Linq;

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
    public GameObject zeros;
    [DllImport("CPlusUtility")]
    public static extern int BinarySearch(int[] arr, int l, int r, int num);

    private void Awake()
    {
        zeros.SetActive(false);
    }

    //When save score button is pushed, pull name from the username box and combine it with the score and upload it to the leaderboard
    //If the score has a 0 in it, cover the screen in zeros
    public void SaveScoreName()
    {
        int scores = StatTracker.Instance.currentScore;
        int[] searchArray = GetIntArray(StatTracker.Instance.currentScore);

        theName = inputField.GetComponent<Text>().text;
        highscores = GetComponent<Highscores>();
        Highscores.AddNewHighscore(theName, scores);

        if (BinarySearch(searchArray, 0, StatTracker.Instance.currentScore.ToString().Length, 0) >= 0)
        {
            zeros.SetActive(true);
        }
    }


    int[] GetIntArray(int num)
    {
        List<int> listOfInts = new List<int>();
        while (num > 0)
        {
            listOfInts.Add(num % 10);
            num = num / 10;
        }
        listOfInts.Reverse();
        return listOfInts.ToArray();
    }
}
