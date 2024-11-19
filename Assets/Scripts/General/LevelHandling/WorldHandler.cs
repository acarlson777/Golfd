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
    [SerializeField] private GameObject _mask;
    [SerializeField] bool debug;
    private Pose levelPosPose;
    private Coroutine updateCurrentLevelPositionToFloorHeightCoroutine = null;

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

        levelIndex = PlayerPrefs.GetInt(_worldIndex.ToString());
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
        if (updateCurrentLevelPositionToFloorHeightCoroutine != null) { StopCoroutine(updateCurrentLevelPositionToFloorHeightCoroutine); }
        yield return AnimateOut();
        yield return AnimateIn();
        updateCurrentLevelPositionToFloorHeightCoroutine = StartCoroutine(UpdateCurrentLevelHeightToFloorHeight());
    }

    private IEnumerator AnimateOut()
    {
        isLevelComplete = false;

        if (currLevelHandler != null)
        {
            yield return currLevelHandler.AnimateOutCoroutine();
            levelIndex++;
        }
    }

    private IEnumerator AnimateIn()
    {
        currLevelHandler = instantiatedLevelList[levelIndex].GetComponent<LevelHandler>();
        currLevelHandler.LEVEL.SetActive(true);
        currLevelHandler.SetAnimateEndHeight(levelPosPose.position.y);
        yield return currLevelHandler.AnimateInCoroutine();
    }

    private IEnumerator UpdateCurrentLevelHeightToFloorHeight()
    {
        GameObject worldFloor = GameObject.FindGameObjectWithTag("WorldFloor");
        while (true)
        {
            currLevelHandler.transform.position = new Vector3(currLevelHandler.transform.position.x, worldFloor.transform.position.y, currLevelHandler.transform.position.z);
            currLevelHandler.SetAnimateEndHeight(worldFloor.transform.position.y);
            yield return null;
        }
    }

    public void UpdateLevelPosition(Pose hitPose)
    {
        //_ENVIRONMENT.transform.position = hitPose.position;
        levelPosPose = hitPose;
        _mask.transform.position = new Vector3(_mask.transform.position.x ,-25 + hitPose.position.y, _mask.transform.position.z);
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
