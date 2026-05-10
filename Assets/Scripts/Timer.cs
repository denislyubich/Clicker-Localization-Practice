using System.Collections;
using TMPro;
using UnityEngine;


public class Timer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float timer;


    private void Awake()
    {
        timerText = GetComponent<TextMeshProUGUI>();
 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = GameManager.instance.startingTimer;
        StartCoroutine(TimerCountdown());
    }


    IEnumerator TimerCountdown()
    {
        while (!GameManager.instance.isGameOver)
        {
            yield return new WaitForSeconds(1);
            timer -= 1;
            timerText.text = ConvertToMinsSecondsFormat();

            if (timer == 0)
            {
                GameManager.instance.GameOver();
            }
        }
    }

    private string ConvertToMinsSecondsFormat()
    {
        float timerToConvert = timer;
        string convertedTimer;
        float mins = (int)(timer / 60);
        float secs = timer - (mins * 60);
        if (secs < 10)
        {
            convertedTimer = mins + ":0" + secs;
        }


        else
        {
            convertedTimer = mins + ":" + secs;
        }

        return convertedTimer;
    }
}
