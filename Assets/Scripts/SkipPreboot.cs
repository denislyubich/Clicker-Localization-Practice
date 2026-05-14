using UnityEngine;
using UnityEngine.SceneManagement;

public static class SkipPreboot
{
    // This is a static script to skip preboot scene in all builds except WebGL

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void SkipFirstSceneOnPlatforms()
    {
#if !UNITY_WEBGL
        SceneManager.LoadScene(1);

#endif
    }

}
