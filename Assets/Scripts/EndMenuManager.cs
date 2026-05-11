using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class EndMenuManager : MonoBehaviour
{
    private int score;
    private int bestScore;
    [SerializeField] private float timeToRestart = 5;



    // UI Texts
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = MainManager.instance.score;

        MainManager.instance.LoadBestScore();

        bestScore = MainManager.instance.bestScore;

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
        if (score >= bestScore)
        {
            MainManager.instance.SaveBestScore();
            bestScoreText.text = "Your best score:" + score;
        }

        else
        {
            bestScoreText.text = "Your best score:" + bestScore;
        }
    }


    void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

}



