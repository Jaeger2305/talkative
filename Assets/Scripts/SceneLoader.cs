using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadLevel(sceneIndex, true, true));
    }
    public void LoadScene(string sceneName)
    {
        LoadScene(sceneName, true, true);
    }

    public void LoadScene(string sceneName, bool unloadCurrent, bool makeMainScene)
    {
        var idx = SceneUtility.GetBuildIndexByScenePath($"Assets/Scenes/{sceneName}.unity");
        StartCoroutine(LoadLevel(idx, unloadCurrent, makeMainScene));
    }

    IEnumerator LoadLevel(int sceneIndex, bool unloadCurrent, bool makeMainScene)
    {
        if (makeMainScene)
        {
            SceneManager.LoadScene("Loading", LoadSceneMode.Additive);
            // give it time to load in the start transition (probably a fade)
            // would be nice to configure this, but this is simple.
            yield return new WaitForSecondsRealtime(3);
        }
        if (unloadCurrent) SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        var asyncLoadLevel = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);

        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading the Scene");
            yield return null;
        }
        if (makeMainScene)
        {
            yield return 0; // wait a frame, so it can finish loading
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
        }
    }
}