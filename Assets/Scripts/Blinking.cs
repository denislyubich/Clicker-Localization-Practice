using System.Collections;
using TMPro;
using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    private RectTransform textTransform;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
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
            textTransform.localScale = Vector3.zero;
            yield return new WaitForSeconds(speed);
            textTransform.localScale = Vector3.one;
        }
    }
}
