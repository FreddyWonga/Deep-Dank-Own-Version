//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System.IO;
//using System.Diagnostics;

//public class DataManager : MonoBehaviour
//{
//    public string file = "highscores.txt";
//    public HighscoreTable data;

//    public void Awake()
//    {
//        Load();
//        Debug.Log(data);
//    }

//    public void Save()
//    {
//        string json = JsonUtility.ToJson(data);
//        WriteToFile(file, json);
//    }

//    public void Load()
//    {
//        data = new HighscoreTable();
//        string json = ReadFromFile(file);
//        JsonUtility.FromJsonOverwrite(json, data);
//    }

//    public void WriteToFile(string fileName, string json)
//    {
//        string path = GetFilePath(fileName);
//        FileStream fileStream = new FileStream(path, FileMode.Create);

//        using (StreamWriter writer = new StreamWriter(fileStream))
//        {
//            writer.Write(json);
//        }
//    }

//    public string ReadFromFile(string fileName)
//    {
//        string path = GetFilePath(fileName);
//        if (File.Exists(path))
//        {
//            using (StreamReader reader = new StreamReader(path))
//            {
//                string json = reader.ReadToEnd();
//                return json;
//            }
//        }
//        else
//        {
//            Debug.LogWarning("Leaderboard Not Found!");
//        }
//        return "";
//    }

//    public string GetFilePath(string fileName)
//    {
//        return Application.persistentDataPath + "/" + fileName;
//    }
//}
