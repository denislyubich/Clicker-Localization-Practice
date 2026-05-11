using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuManager : MonoBehaviour
{
    private int score;
    private string playerName;
    [SerializeField] private float timeToRestart = 5;


    // UI Texts
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = MainManager.instance.score;
        playerName = MainManager.instance.playerName;

        finalScoreText.text = "Your final score:" + score;

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
        bestScoreText.text = "Best Score:" + data.highScores[0].score + " by <color=blue>" + data.highScores[0].playerName + "</color>";
     }

    void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}



