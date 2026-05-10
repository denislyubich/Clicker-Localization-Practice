using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Floats and ints
    public float startingTimer = 10;
    [HideInInspector] public int score;
    [HideInInspector] public int bestScore;
    [SerializeField] private float timeToRestart = 5;

    // UI Screens
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject endScreen;

    // UI Texts
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    

    [HideInInspector]public bool isGameOver;

    private void Awake()
    {
        instance = this;
        score = 0;
    }

    private void Start()
    {
        scoreText.text = "Score:" + score;
        gameScreen.SetActive(true);
        endScreen.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isGameOver)
        {
            UpdateScore();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    private void UpdateScore()
    {
        score++;
        scoreText.text = "Score:" + score;
        Debug.Log("Score: " + score);
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
        isGameOver = true;

        DataManager.instance.LoadBestScore();

        finalScoreText.text = "Your final score:" + score;

        PrintBestScore();

        gameScreen.SetActive(false);
        endScreen.SetActive(true);

        Invoke("RestartGame", timeToRestart);
    }

    void PrintBestScore()
    {
        if (score >= bestScore)
        {
            DataManager.instance.SaveBestScore();
            bestScoreText.text = "Your best score:" + score;
        }

        else
        {
            bestScoreText.text = "Your best score:" + bestScore;
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void ExitGame()
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
}
