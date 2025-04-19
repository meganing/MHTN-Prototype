using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Obsolete("Please call scene change directly.")]
public class SceneLoader : MonoBehaviour
{
    [Obsolete("Please call scene change directly.")]
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    [Obsolete("Please call scene change directly.")]
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}