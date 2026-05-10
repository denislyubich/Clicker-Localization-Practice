using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private string saveFilePath;

    private void Awake()
    {
        instance = this;
        saveFilePath = Application.persistentDataPath + "/saveFile.json";
    }

    [System.Serializable]
    class SaveData
    {
        public int score;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.score = GameManager.instance.score;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadBestScore()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            GameManager.instance.bestScore = data.score;
        }
       

    }
}
