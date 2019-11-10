using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private Slider loadingBar;
    [SerializeField] private GameObject loadText;
    bool canStart;
    AsyncOperation ao;
    private void Update()
    {
        if (canStart)
        {
            if (Input.anyKey)
            {
                ao.allowSceneActivation = true;
            }
        }
    }
    public void PlayButton()
    {
        Destroy(mainCanvas);
        StartCoroutine(loadLevel(1));
    }
    IEnumerator loadLevel(int level)
    {
        loadingCanvas.SetActive(true);
        ao = SceneManager.LoadSceneAsync(level);
        ao.allowSceneActivation = false;

        while (ao.isDone == false)
        {
            loadingBar.value = ao.progress;
            if (ao.progress >= 0.9f)
            {
                loadingBar.value = 1f;
                loadText.SetActive(true);
                canStart = true;
            }
            yield return null;
        }
    }
    public void ExitButton()
    {
        Application.Quit();
    }
}
