using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    private int score;
    private string playerName;

    [HideInInspector] private int bestScore;
    [HideInInspector] private string bestPlayerName;
    [SerializeField] private LocalizeStringEvent bestScoreText;

    [SerializeField] private float timeToRestart = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = MainManager.instance.score;
        playerName = MainManager.instance.playerName;

        (MainManager.instance.localizationVarSource["global"]["score"] as IntVariable).Value = score;

        PrintBestScore();

        Invoke("RestartGame", timeToRestart);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainManager.instance.ExitToTheMainMenu();
        }
    }

    void PrintBestScore()
    {
        // Add current score to the highscores list
        MainManager.instance.AddNewScore(playerName, score);
        // Reload the highscores list with newly updated and ranked score
        MainManager.HighScoreList data = MainManager.instance.LoadScores();

        // Print best score
        bestScore = data.highScores[0].score;
        bestPlayerName = data.highScores[0].playerName;

        // Update global variables for localization
        (MainManager.instance.localizationVarSource["global"]["best-score"] as IntVariable).Value = bestScore;
        (MainManager.instance.localizationVarSource["global"]["best-player-name"] as StringVariable).Value = bestPlayerName;

    }

    void RestartGame()
    {
        SceneManager.LoadScene(2);
    }

}



