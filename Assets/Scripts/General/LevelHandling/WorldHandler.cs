using UnityEngine;
using System.Collections;

public class WorldHandler : MonoBehaviour
{
    public static WorldHandler Instance { get; private set; }

    [SerializeField] private int _worldIndex;
    private int levelIndex = 0;
    [SerializeField] private GameObject[] _levelList;
    private GameObject[] instantiatedLevelList;
    [SerializeField] private int _strokeCount = 0;
    private LevelHandler currLevelHandler = null;
    public bool isLevelComplete = true;
    [SerializeField] private GameObject _ENVIRONMENT;
    [SerializeField] bool debug;

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
        instantiatedLevelList = new GameObject[_levelList.Length];
        for (int i = 0; i < _levelList.Length; i++)
        {
            GameObject nextLevel = Instantiate(_levelList[i], _levelList[i].transform.position, _levelList[i].transform.rotation, _ENVIRONMENT.transform);
            nextLevel.GetComponent<LevelHandler>().LEVEL.SetActive(false);
            instantiatedLevelList[i] = nextLevel;
        }
    }

    private void Update()
    {
        if (debug) { Time.timeScale = 0.1f; } else { Time.timeScale = 1; }
    }

    public void OnLevelCompleted()
    {
        int score = CalculateScore();
        _strokeCount = 0;
        isLevelComplete = true;
        LoadNextLevel();
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        yield return AnimateOut(); //Remove movement of animations and revert back to animationless to make sure the level always reaches the right height after tap
        yield return AnimateIn();
    }

    private IEnumerator AnimateOut()
    {
        isLevelComplete = false;

        if (currLevelHandler != null)
        {
            yield return currLevelHandler.AnimateOutCoroutine();
            currLevelHandler.LEVEL.SetActive(false);
            levelIndex++;
        }
    }

    private IEnumerator AnimateIn()
    {
        currLevelHandler = instantiatedLevelList[levelIndex].GetComponent<LevelHandler>();
        currLevelHandler.LEVEL.SetActive(true);
        yield return currLevelHandler.AnimateInCoroutine();
    }

    public void UpdateLevelPosition(Pose hitPose)
    {
        _ENVIRONMENT.transform.position = hitPose.position;
        //_ENVIRONMENT.transform.rotation = hitPose.rotation;

        Debug.Log("Floor Height" + hitPose.position);
    }

    public void IncrementStrokeCount()
    {
        _strokeCount++;
    }

    private int CalculateScore()
    {
        return _strokeCount - currLevelHandler.par;
    }

    public LevelHandler GetCurrLevelHandler()
    {
        return currLevelHandler;
    }
}
