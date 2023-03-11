using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private float transitionTime = 3f;

    void Start()
    {
        LoadLoadingScreen();
        StartCoroutine(Unload());
}

    public void LoadLoadingScreen()
    {
        transitionAnimator.SetTrigger("transitionIn");
    }


    IEnumerator Unload()
    {
        yield return new WaitForSecondsRealtime(transitionTime);

        transitionAnimator.SetTrigger("transitionOut");

        yield return new WaitForSecondsRealtime(transitionTime-1);
        SceneManager.UnloadSceneAsync("Loading");
    }
}