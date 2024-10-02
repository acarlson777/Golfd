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
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        _transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        scene.allowSceneActivation = true;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }
}
