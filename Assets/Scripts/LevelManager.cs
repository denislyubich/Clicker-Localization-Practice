using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.SmartFormat.Extensions;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
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
        (MainManager.instance.localizationVarSource["global"]["score"] as IntVariable).Value = 0;
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

        // Update global variables for localization
        (MainManager.instance.localizationVarSource["global"]["score"] as IntVariable).Value = score;
        
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
