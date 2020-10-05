using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private LogReader reader;
    public LogReader logReader;
    public TextFileManager textfileManager;
    


    private void Start()
    {
        var leaderboardScores = new List<string>();
        reader = GetComponent<LogReader>();
        for (int i = 0; i < textfileManager.logContents.Length; i++)
        {
            leaderboardScores.Add(textfileManager.logContents[i]);
        }
    }
}
