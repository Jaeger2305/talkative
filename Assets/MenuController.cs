using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuController : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    private bool isLoading = false;
    void Update()
    {
        if (Input.anyKey && !isLoading)
        {
            sceneLoader.LoadScene(1);
            isLoading = true;
            Destroy(gameObject);
        }
    }
}
