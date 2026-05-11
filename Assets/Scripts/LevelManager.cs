using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Floats and ints
    public int startingTimer = 10;

    private int score;
    [SerializeField] private TextMeshProUGUI scoreText;

    public static LevelManager instance;

    [HideInInspector]public bool isGameOver;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        MainManager.instance.score = 0;
        scoreText.text = "Score:" + score;
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
        scoreText.text = "Score:" + score;
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
