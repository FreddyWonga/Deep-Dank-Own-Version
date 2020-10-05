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
    public LogReader logReader;
    public Score score;
    public StatTracker StatTracker;
    public GameObject SaveButton;

    public void SaveScoreName()
    {
        //theName = inputField.GetComponent<Text>().text;
        //int scores = GameObject.Find("Stat Tracker").GetComponent<StatTracker>().currentScore;
        //UnityEngine.Debug.Log(scores);
        //string contents = scores;
        //string saveKey = ((logReader.fileManager.logContents.Length + 1).ToString() + theName);
        //logReader.SaveKeyValuePair(saveKey, contents);
        //SaveButton.SetActive(false);

        int playerNumber = logReader.fileManager.logContents.Length + 1;
        int scores = GameObject.Find("Stat Tracker").GetComponent<StatTracker>().currentScore;
        string contents = scores.ToString();
        theName = inputField.GetComponent<Text>().text;
        string playerName = "PlayerName" + playerNumber.ToString();
        UnityEngine.Debug.Log(playerName);
        logReader.SaveKeyValuePair(playerName, theName);
        logReader.SaveKeyValuePair(theName, scores.ToString());
    }
}
