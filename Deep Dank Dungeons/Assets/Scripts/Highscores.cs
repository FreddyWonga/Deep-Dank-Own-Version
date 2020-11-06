using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class Highscores : MonoBehaviour
{
    [DllImport("CPlusUtility")]
    public static extern int BinarySearch(int[] arr, int l, int r, int num);
    [DllImport("CPlusUtility")]
    public static extern void BubbleSort(int[] arr, int n);

    const string privateCode = "6XExyQIZ6E-roaQTBkfJXgdlhGCCt6d0yWC9cyXi0Z6g";
    const string publicCode = "5f827891eb371809c4771a8e";
    const string webURL = "http://dreamlo.com/lb/";
    
    public Highscore[] highscoresList;
    static Highscores instance;
    DisplayHighscores highscoresDisplay;

    private void Awake()
    {
        instance = this;
        highscoresDisplay = GetComponent<DisplayHighscores>();
    }

    public static void AddNewHighscore(string username, int score)
    {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

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

    public void DownloadHighScores()
    {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }

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
		string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];

		for (int i = 0; i <entries.Length; i ++) {
			string[] entryInfo = entries[i].Split(new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username,score);
			print (highscoresList[i].username + ": " + highscoresList[i].score);
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
            for(j = 0; j < scoreArray.Length; j++)
            {
                if (highscoresList[k].score == scoreArray[j])
                {
                    highscoresList[k].score = scoreArray[j];
                    break;
                }
            }
        }
    }
}

public struct Highscore {
	public string username;
	public int score;

	public Highscore(string _username, int _score) {
		username = _username;
		score = _score;
	}

}