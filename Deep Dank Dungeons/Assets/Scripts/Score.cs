using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Score : MonoBehaviour
{
    public static int scoreValue = 0;
    public int currentScore;
    
    Text scoreSplash;

    void Start()
    {
        scoreSplash = GetComponent<Text>();
    }


    void Update()
    {
        scoreSplash.text = "" + scoreValue;
        currentScore = scoreValue;
    }
}
