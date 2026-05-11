using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EndMenuManager : MonoBehaviour
{
    private int score;
    private int bestScore;
    private string playerName;
    private string bestPlayerName;
    [SerializeField] private float timeToRestart = 5;



    // UI Texts
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = MainManager.instance.score;
        playerName = MainManager.instance.playerName;

        MainManager.instance.LoadBestScore();

        bestScore = MainManager.instance.bestScore;
        bestPlayerName = MainManager.instance.bestPlayerName;

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
        if (score >= bestScore) // if best score is your current score
        {
            MainManager.instance.SaveBestScore();
            bestScoreText.text = "Best Score:" + score + " by <color=blue>" + playerName + "</color>";
        }

        else // if best score is not your current score
        {
            bestScoreText.text = "Best Score:" + bestScore + " by <color=blue>" + bestPlayerName + "</color>";
        }
    }


    void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}



