using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class Highscores : MonoBehaviour
{
    [DllImport("CPlusUtility")]
    public static extern int BinarySearch(int[] arr, int l, int r, int num);
    [DllImport("CPlusUtility")]
    public static extern void BubbleSort(int[] arr, int n);
    [DllImport("CPlusUtility")]
    public static extern void quickSortInvert(int[] arr, int low, int high);

    const string privateCode = "6XExyQIZ6E-roaQTBkfJXgdlhGCCt6d0yWC9cyXi0Z6g";
    const string publicCode = "5f827891eb371809c4771a8e";
    const string webURL = "http://dreamlo.com/lb/";
    
    public Highscore[] highscoresList;
    static Highscores instance;
    DisplayHighscores highscoresDisplay;
    [SerializeField]
    public SortHighscores sortHighscores;
    public Highscore[] downloadedScores;
    public bool randomize = false;


    public void Awake()
    {
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighscores>();
        sortHighscores = GameObject.Find("GameObject (1)").GetComponent<SortHighscores>();
    }


    //Start the Coroutine for uploading a new score
    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    //Upload the username and score to the cloud
    IEnumerator UploadNewHighscore(string username, int score)
    {
        WWW WWW = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
        yield return WWW;

        if (string.IsNullOrEmpty(WWW.error))
        {
            print("Upload Successful");
            DownloadHighScores();
        }
        else
        {
            print("Error Uploading" + WWW.error);
        }
    }
    //Starts the downloading highscore Coroutine
    public void DownloadHighScores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

    //Download the data from the cloud and pass it to the FormatHighscores function to sort
    //When sorted pass to the highscoreDisplay script to be used to update the leaderboard
    IEnumerator DownloadHighscoresFromDatabase()
    {
        WWW WWW = new WWW(webURL + publicCode + "/pipe/");
        yield return WWW;

        if (string.IsNullOrEmpty(WWW.error))
        {
            FormatHighscores(WWW.text);

            highscoresDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error Downloading" + WWW.error);
        }
    }

	void FormatHighscores(string textStream) 
    {
        //If the randomize button has been pushed and randomize is true then randomly switch the top 10 scores position on the leaderboard
        if (randomize == true)
        {
            int i;
            int randomNum;
            for (i = 0; i < highscoresList.Length; i++)
            {
                randomNum = UnityEngine.Random.Range(0, highscoresList.Length);
                Highscore temp = highscoresList[i];
                highscoresList[i] = highscoresList[randomNum];
                highscoresList[randomNum] = temp;
            }
        }
        //For the first sorting or every 2nd push of the sort button, split the scores from the usernames and sort the scores using a bubble sort
        //Then match the scores and usernames back together in the correct place inorder
        if (sortHighscores.invertSort == false & randomize == false)
        {
            string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            highscoresList = new Highscore[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                highscoresList[i] = new Highscore(username, score);
            }

            int k;
            LinkedList<int> scores = new LinkedList<int>();
            int[] scoreArray = new int[highscoresList.Length];

            for (k = 0; k < highscoresList.Length; k++)
            {
                scores.AddLast(highscoresList[k].score);
            }

            scores.CopyTo(scoreArray, 0);

            BubbleSort(scoreArray, scoreArray.Length);

            int j;
            for (k = 0; k < highscoresList.Length; k++)
            {
                for (j = 0; j < scoreArray.Length; j++)
                {
                    if (highscoresList[k].score == scoreArray[j])
                    {
                        highscoresList[k].score = scoreArray[j];
                    }
                }
            }
        }

        //For the first push of the sort button and every 2nd push after that, split the scores from the usernames and sort the scores using a quick sort
        //Then match the scores and usernames back together in the correct place inorder
        if (sortHighscores.invertSort == true & randomize == false)
        {
            Debug.Log("Inverted sorting");
            string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            highscoresList = new Highscore[entries.Length];

            for (int i = 0; i < entries.Length; i++)
            {
                string[] entryInfo = entries[i].Split(new char[] { '|' });
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                highscoresList[i] = new Highscore(username, score);
                
            }

            int k;
            LinkedList<int> scores = new LinkedList<int>();
            int[] scoreArray = new int[highscoresList.Length];

            for (k = 0; k < highscoresList.Length; k++)
            {
                scores.AddLast(highscoresList[k].score);
            }

            scores.CopyTo(scoreArray, 0);

            quickSortInvert(scoreArray, 0, highscoresList.Length);
            int j;
            for (k = 0; k < highscoresList.Length; k++)
            {
                for (j = 0; j < scoreArray.Length; j++)
                {
                    if (highscoresList[k].score == scoreArray[j])
                    {
                        highscoresList[k].score = scoreArray[j];
                    }
                }
            }
        }
        randomize = false;
    }
}

public struct Highscore {
	public string username;
	public int score;

	public Highscore(string _username, int _score) 
    {
		username = _username;
		score = _score;
	}

}