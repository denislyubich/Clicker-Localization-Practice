using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
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
        // bestScoreText.text = "Best Score:" + data.highScores[0].score + " by <color=blue>" + data.highScores[0].playerName + "</color>";
        bestScoreText.StringReference.Arguments = new object[] { score, bestScore, bestPlayerName };
        // Force the UI to refresh the text with the new argument
        bestScoreText.StringReference.RefreshString();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}



