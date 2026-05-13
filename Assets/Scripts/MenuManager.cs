using TMPro;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.PersistentVariables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField enterYourNameField;
    [SerializeField] private TextMeshProUGUI bestPlayersText;

    int playerRank = 0; // Initialized int to dynamically store ranks in the highscore table

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Initialize game player data, reset it to zero and null when reloading main menu
        MainManager.instance.score = 0;
        (MainManager.instance.localizationVarSource["global"]["score"] as IntVariable).Value = 0;
        MainManager.instance.playerName = null;

        // Print highscores
        MainManager.HighScoreList data = MainManager.instance.LoadScores();
        foreach (MainManager.HighScoreEntry entry in data.highScores)
        {
            playerRank++;
            bestPlayersText.text += "0" + playerRank + " - " + entry.playerName + ":" + entry.score + "\n";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AssignPlayerName();
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainManager.instance.ExitGame();
        }
    }

    void AssignPlayerName()
    {
        if (string.IsNullOrEmpty(enterYourNameField.text)) // Assign Anonymus if player doesn't write anything
        {
            MainManager.instance.playerName = "Anonymus";
        }

        else
        {
            MainManager.instance.playerName = enterYourNameField.text;
        }

        Debug.Log("Player name is " + MainManager.instance.playerName);
    }
}
