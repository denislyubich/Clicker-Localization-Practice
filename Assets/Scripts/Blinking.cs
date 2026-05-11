using System.Collections;
using TMPro;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private TextMeshProUGUI text;
    private string startingText;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        startingText = text.text;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed);
            text.text = "";
            yield return new WaitForSeconds(speed);
            text.text = startingText;
        }
    }
}
