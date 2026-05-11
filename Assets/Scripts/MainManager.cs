using System;
using UnityEditor;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [HideInInspector] public int score;
    [HideInInspector] public int bestScore;
    [HideInInspector] public string playerName;
    [HideInInspector] public string bestPlayerName;

    public static MainManager instance;
    private string saveFilePath;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = Application.persistentDataPath + "/saveFile.json";

        // Initial data
        score = 0;
        playerName = "Anonymus";
    }

    public void ExitToTheMainMenu()
    {
        score = 0;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        {
            EditorApplication.ExitPlaymode();
        }

#elif UNITY_WEBGL
{

}

#else
{
Application.Quit();
}

#endif

    }

    [System.Serializable]
    class SaveData
    {
        public int score;
        public string playerName;
    }

    public void SaveBestScore()
    {
        SaveData data = new SaveData();
        data.score = score;
        data.playerName = playerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
    }

    public void LoadBestScore()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestScore = data.score;
            bestPlayerName = data.playerName;
        }


    }
}
