using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreDisplay : MonoBehaviour

{
    public static int scoreValue;
    public int currentScore;

    public StatTracker StatTracker;

    Text scoreSplash;

    void Awake()
    {
        scoreSplash = GetComponent<Text>();
        scoreValue = StatTracker.Instance.scoreValue;
        currentScore = StatTracker.Instance.currentScore;
    }


    void Update()
    {
        scoreSplash.text = "" + scoreValue;
    }
}
