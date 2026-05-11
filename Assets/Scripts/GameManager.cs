using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //Floats and ints
    [HideInInspector] public int score;
    [HideInInspector] public int bestScore;
    public int startingTimer;
    [SerializeField] private float timeToRestart = 5;

    [SerializeField] private GameObject[] gameScreens;
    [SerializeField] public static bool isNameEntered;

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
        if (!isNameEntered)
        {
            LoadGameScreen(0);
        }

        else
        {
            LoadGameScreen(1);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && gameScreens[1].activeSelf == true)
        {
            UpdateScore();
        }

        if (Input.GetKeyDown(KeyCode.Return) && gameScreens[0].activeSelf == true)
        {
            isNameEntered = true;
            LoadGameScreen(1);
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }

    void LoadGameScreen(int i)
    {
        foreach (GameObject gameScreen in gameScreens)
        {
            gameScreen.SetActive(false);
        }

        gameScreens[i].SetActive(true);

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

        LoadGameScreen(2);

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
