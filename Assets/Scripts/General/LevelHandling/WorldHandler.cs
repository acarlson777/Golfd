using UnityEngine;
using System.Collections;

public class WorldHandler : MonoBehaviour
{
    public static WorldHandler Instance { get; private set; }

    [SerializeField] private int _worldIndex;
    private int _levelIndex = 0;
    [SerializeField] private GameObject[] _levelList;
    [SerializeField] private int _strokeCount = 0;
    private LevelHandler currLevelHandler = null;

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

        //Need to instantiate all golf levels at their animateout positions at the very beginning
        for (int i = 0; i < _levelList.Length; i++)
        {
            _levelList[i].SetActive(false);
        }
    }

    private void Start()
    {
        //LoadNextLevel();
    }

    public void OnLevelCompleted()
    {
        int score = CalculateScore();
        _strokeCount = 0;
        //Show Level Complete Screen whilst getting strokeCount
    }

    public void LoadNextLevel()
    {
        if (currLevelHandler != null) {
            currLevelHandler.AnimateOut();
            _levelList[_levelIndex].SetActive(false);
        }
        _levelIndex++;
        _levelList[_levelIndex].SetActive(true);
        currLevelHandler = _levelList[_levelIndex].GetComponent<LevelHandler>();
        currLevelHandler.AnimateIn();
    }

    public void IncrementStrokeCount()
    {
        _strokeCount++;
    }

    private int CalculateScore()
    {
        return _strokeCount - currLevelHandler.par;
    }

    public GameObject GetCurrentLevelGameObject()
    {
        return _levelList[_levelIndex];
    }
}
