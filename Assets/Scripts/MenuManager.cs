using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private TMP_InputField enterYourNameField;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainManager.instance.score = 0;
        MainManager.instance.playerName = null;
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
        if (string.IsNullOrEmpty(enterYourNameField.text))
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
