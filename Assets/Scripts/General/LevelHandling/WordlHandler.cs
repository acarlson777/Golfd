using UnityEngine;
using System.Collections;

public class WorldHandler : MonoBehaviour
{
    public static WorldHandler Instance { get; private set; }

    [SerializeField] private int _levelIndex = -1;
    [SerializeField] private GameObject[] _levelList;
    private LevelHandler currLevelHandler;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Start()
    {
        LoadNextLevel();
    }

    public void OnLevelCompleted()
    {
        //Show Level Complete Screen whilst getting strokeCount
    }

    private void LoadNextLevel()
    {
        if (currLevelHandler != null) { currLevelHandler.AnimateOut(); }
        _levelIndex++;
        currLevelHandler = _levelList[_levelIndex].GetComponent<LevelHandler>();
        currLevelHandler.AnimateIn();
    }
}
