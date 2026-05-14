using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.SceneManagement;

public class PrebootManager : MonoBehaviour
{

    // This script is for WEBGL only. It loads localization settings before the game starts. The scene
    // is skipped in other builds

    void Start()
    {

        //Though I have a SkipPreboot script I keep it here as a failsafe
    #if !UNITY_WEBGL
        SceneManager.LoadScene(1);

    #else
        StartCoroutine(InitializeLocalizationRoutine());

    #endif
    }

    private IEnumerator InitializeLocalizationRoutine()
    {
        yield return LocalizationSettings.InitializationOperation;
        SceneManager.LoadScene(1);
    }

}
