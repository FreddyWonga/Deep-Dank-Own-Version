using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameTransfer : MonoBehaviour
{
    public string theName;
    [SerializeField]
    public GameObject inputField;
    //public script HighscoreTable;
    //public script Score;
    public Score score;
    public HighscoreTable highscore;
    public int addScore;

    void Start()
    {
        highscore = GetComponent<HighscoreTable>();
        score = GetComponent<Score>();
    }

    public void StoreName()
    {
        theName = inputField.GetComponent<Text>().text;

        highscore.AddHighscoreEntry(score.currentScore, theName);
    }
}
