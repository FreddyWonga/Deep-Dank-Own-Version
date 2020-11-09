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
        scoreValue = StatTracker.Instance.scoreValue;
        currentScore = StatTracker.Instance.currentScore;
    }

    //Update the score on the HUD to display the current score
    void Update()
    {
        scoreSplash.text = "" + scoreValue;
        currentScore = scoreValue;
        StatTracker.Instance.scoreValue = scoreValue;
        StatTracker.Instance.currentScore = currentScore;
    }
}
