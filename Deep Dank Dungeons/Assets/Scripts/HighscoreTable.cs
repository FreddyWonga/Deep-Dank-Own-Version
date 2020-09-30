using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using Debug = UnityEngine.Debug;
using System.IO;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    //private List<HighscoreEntry> highscoreEntryList;
    private List<Transform> highscoreEntryTransformList;
    public DataManager dataManager;

    //public string[] ReadFileContents(string fileName)
    //{
    //    string dirPath = Application.dataPath + "/Resources/" + fileName + ".txt";
    //    string[] tContents = new string[0];
    //    if (File.Exists(dirPath) == true)
    //    {
    //        tContents = File.ReadAllLines(dirPath);
    //    }
    //    logContents = tContents;
    //    return tContents;
    //}

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        Highscores highscores = dataManager.Load();


        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        //Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        // Sort Entry list by Score
        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score > highscores.highscoreEntryList[i].score)
                {
                    //Swap
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 40f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;

        transformList.Add(entryTransform);
    }

    public void AddHighscoreEntry(int score, string name)
    {
        // Create HighscoreEntry
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

    //    //Load Saved Highscores
    //    string jsonString = PlayerPrefs.GetString("highscoreTable");
    //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        //    // Add new entry to Highscores
        //    highscores.highscoreEntryList.Add(highscoreEntry);

        //    //Save Updated Highscores
        //    string json = JsonUtility.ToJson(highscores);
        //    PlayerPrefs.SetString("highscoreTable", json);
        //    PlayerPrefs.Save();

        //    BinaryWriter bw;
        //    try
        //    {
        //        bw = new BinaryWriter(new FileStream("highscores.txt", FileMode.Create));
        //        for (int i = 0; i < highscores.highscoreEntryList.count; i++)
        //        {
        //            bw.Write(highscores.highscoreEntryList[i]);
        //        }
        //    }
        //    catch (Exception fe)
        //    {
        //        return;
        //    }
        //    bw.Close();

        //}


        //public void AddKeyValuePair(string highscores, string name, string score)
        //{
        //    ReadFileContents(fileName);
        //    string dirPath = Application.dataPath + highscores + ".txt";
        //    string tContents = name + "," + score;
        //    if (File.Exists(dirPath) == true)
        //    {
        //        bool contentsFound = false;
        //        for (int i = 0; i < logContents.Length; i++)
        //        {
        //            if (logContents[i].Contains(name) == true)
        //            {
        //                logContents[i] = " - " + tContents;
        //                contentsFound = true;
        //            }
        //        }

        //        if (contentsFound == true)
        //        {
        //            File.WriteAllLines(dirPath, logContents);
        //        }
        //        else
        //        {
        //            File.AppendAllText(dirPath, " - " + tContents + "\n");
        //        }
        //    }
        }


    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

    
}
