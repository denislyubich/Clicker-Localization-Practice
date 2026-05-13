using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Floats and ints
    public int startingTimer = 10;

    [HideInInspector] public int score;
    [SerializeField] private LocalizeStringEvent scoreText;

    public static LevelManager instance;

    [HideInInspector]public bool isGameOver;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //Resetting the score
        MainManager.instance.score = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UpdateScore();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);   
        }
    }

    void UpdateScore()
    {
        score++;
        scoreText.StringReference.Arguments = new object[] { score };
        // Force the UI to refresh the text with the new argument
        scoreText.StringReference.RefreshString();
        Debug.Log("Score: " + score);
        MainManager.instance.score = score;
    }

    public void GameOver()
    {
        Debug.Log("Game over!");
        isGameOver = true;
        SceneManager.LoadScene(2);
    }

}
