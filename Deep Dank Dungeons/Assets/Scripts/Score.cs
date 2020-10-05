using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public static int scoreValue;
    public int currentScore;

    public StatTracker StatTracker;

    Text scoreSplash;

    void Start()
    {
        scoreSplash = GetComponent<Text>();
        scoreValue = GameObject.Find("Stat Tracker").GetComponent<StatTracker>().scoreValue;
        currentScore = GameObject.Find("Stat Tracker").GetComponent<StatTracker>().currentScore;
    }


    void Update()
    {
        scoreSplash.text = "" + scoreValue;
        currentScore = scoreValue;
        GameObject.Find("Stat Tracker").GetComponent<StatTracker>().scoreValue = scoreValue;
        GameObject.Find("Stat Tracker").GetComponent<StatTracker>().currentScore = currentScore;
    }
}
