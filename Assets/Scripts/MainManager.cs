using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.SceneManagement;


public class MainManager : MonoBehaviour
{
    [HideInInspector] public int score;
    [HideInInspector] public int bestScore;
    [HideInInspector] public string playerName;
    [HideInInspector] public string bestPlayerName;

    [SerializeField] private int highScoreLinesCount = 3;

    [HideInInspector]public PersistentVariablesSource localizationVarSource;

    public static MainManager instance;
    private string saveFilePath;

    // In the WebGL version access jslib plugin to force-refresh browser data library to save data between sessions
    #if UNITY_WEBGL && !UNITY_EDITOR
    // Import the JS function from your .jslib file
    [DllImport("__Internal")]
    private static extern void SyncFiles();
    #endif

    private void Awake()
    {
        // Singleton pattern start
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        // Singleton pattern end

        saveFilePath = Application.persistentDataPath + "/saveFile.json";

        // Initialize localization settings in all builds except WebGL
        #if !UNITY_WEBGL
        LocalizationSettings.InitializationOperation.WaitForCompletion();
        #endif

        // Get default language pack or language pack previously selected by player
        ChangeLanguage(PlayerPrefs.GetInt("GameLanguageIndex", 0));

        // Get source variables for localization
        localizationVarSource = LocalizationSettings.StringDatabase.SmartFormatter.GetSourceExtension<PersistentVariablesSource>();

        // Initial data for playtesting without loading Main Menu level
        score = 0;
        playerName = "Anonymous";
        (localizationVarSource["global"]["score"] as IntVariable).Value = 0;

    }

    public void ChangeLanguage(int index)
    {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
            Debug.Log("Language index is " + LocalizationSettings.SelectedLocale);
            PlayerPrefs.SetInt("GameLanguageIndex", index);
            PlayerPrefs.Save();
    }

    public void ResetScore()
    {
        score = 0;
        (localizationVarSource["global"]["score"] as IntVariable).Value = 0;
    }

    public void ExitToTheMainMenu()
    {
        score = 0;
        SceneManager.LoadScene(1);
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
    public class HighScoreEntry
    {
        public int score;
        public string playerName;
    }

    [System.Serializable]
    public class HighScoreList
    {
        public List<HighScoreEntry> highScores = new List<HighScoreEntry>();
    }

    public void AddNewScore(string name, int score)
    {
        // 1. Load existing data
        HighScoreList data = LoadScores();

        // 2. Add the new entry
        data.highScores.Add(new HighScoreEntry { playerName = name, score = score });

        // 3. Optional: Sort and limit the list
        data.highScores.Sort((x, y) => y.score.CompareTo(x.score));
        if (data.highScores.Count > highScoreLinesCount) data.highScores.RemoveAt(highScoreLinesCount);

        // 4. Save back to file
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);

        #if UNITY_WEBGL && !UNITY_EDITOR
        {
            SyncFiles();
        }

        #endif
    }

    public HighScoreList LoadScores()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<HighScoreList>(json);
        }
      
            return new HighScoreList(); // Return empty list if no file exists
    }

}

