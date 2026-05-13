using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.SceneManagement;

public class PrebootManager : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(InitializeLocalizationRoutine());

        SceneManager.LoadScene(1);
    }

    private IEnumerator InitializeLocalizationRoutine()
    {

#if UNITY_WEBGL
        yield return LocalizationSettings.InitializationOperation;

#else
        yield break;

#endif
    }

}
