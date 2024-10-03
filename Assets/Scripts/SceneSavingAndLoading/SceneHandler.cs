using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance { get; private set; }

    [SerializeField] private GameObject _loadingScreenCanvas;
    [SerializeField] private Animator _transition;
    [SerializeField] private float _transitionTime;

    // Use this tutorial: https://www.youtube.com/watch?v=CE9VOZivb3I

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        Scene currScene = SceneManager.GetActiveScene();
        var nextScene = SceneManager.LoadSceneAsync(sceneName);
        nextScene.allowSceneActivation = false;

        _transition.SetTrigger("Start");
        _transition.ResetTrigger("End");

        yield return new WaitForSeconds(_transitionTime);

        nextScene.allowSceneActivation = true;

        SceneManager.UnloadSceneAsync(currScene);

        _transition.SetTrigger("End");
        _transition.ResetTrigger("Start");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }
}
