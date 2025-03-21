﻿using UnityEngine;
using System.Collections;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class WorldHandler : MonoBehaviour
{
    public static WorldHandler Instance { get; private set; }

    [SerializeField] private int _worldIndex;
    private int levelIndex = 0;
    [SerializeField] private GameObject[] _levelList;
    private GameObject[] instantiatedLevelList;
    [SerializeField] private int _strokeCount = 0;
    public LevelHandler currLevelHandler = null;
    public bool isLevelComplete = true;
    [SerializeField] private GameObject _ENVIRONMENT;
    [SerializeField] private GameObject _mask;
    [SerializeField] bool debug;
    private Pose levelPosPose;
    private Coroutine updateCurrentLevelPositionToFloorHeightCoroutine = null;
    private Vector3 lastKnownBallPos;

    [SerializeField] TextMeshProUGUI levelParText;
    [SerializeField] TextMeshProUGUI strokeCountText;

    [SerializeField] DialogueWrapper dialogueWrapper;
    [SerializeField] ClubHandler clubHandler;
    [SerializeField] private bool strokeCountBasedDialogue; 

    public GolfBallIndicatorHandler ballIndicatorHandler;

    public AnimationController strokeAnimationController;
    public AnimationController parAnimationController;

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
        JsonSerializer.Instance.LoadByJSON();

        instantiatedLevelList = new GameObject[_levelList.Length];
        for (int i = 0; i < _levelList.Length; i++)
        {
            GameObject nextLevel = Instantiate(_levelList[i], _levelList[i].transform.position, _levelList[i].transform.rotation, _ENVIRONMENT.transform);
            //nextLevel.transform.localPosition = new Vector3(0, 0, 0);
            nextLevel.GetComponent<LevelHandler>().LEVEL.SetActive(false);
            instantiatedLevelList[i] = nextLevel;
        }

        levelIndex = PlayerPrefs.GetInt(_worldIndex.ToString());
    }

    private void Update()
    {
        if (debug) { Time.timeScale = 0.1f; } else { Time.timeScale = 1; }
    }

    public void OnLevelCompleted(){

        clubHandler.clubEnabled = false;
        clubHandler._clubHead.SetActive(false);
        ballIndicatorHandler.gameObject.SetActive(false);
        
        int score = CalculateScore();
        if (score > JsonSerializer.Instance.golfPlayerData.WORLDS[_worldIndex-1].LEVELS[levelIndex].bestScore){

            JsonSerializer.Instance.golfPlayerData.WORLDS[_worldIndex-1].LEVELS[levelIndex].bestScore = score;
            JsonSerializer.Instance.SaveByJSON();
        }

        if (levelIndex == _levelList.Length - 1){
            SceneHandler.Instance.LoadScene("LevelSelect");
        }

        //Show some sort of new best animation on screen if score was new best (conffetti would be fun)
        //Constantly show par and current stroke count on the screen (this is a general note)
        isLevelComplete = true;
        StartEndLevelDialogue();
        _strokeCount = 0;
    }

    private void StartEndLevelDialogue()
    {
        // clubHandler.clubEnabled = false;
        // clubHandler._clubHead.SetActive(false);
        int par = JsonSerializer.Instance.golfPlayerData.WORLDS[_worldIndex].LEVELS[levelIndex].PAR;
        if (strokeCountBasedDialogue)
        {
            if (_strokeCount > par)
            {
                dialogueWrapper.StartDialogueSequence(_worldIndex + "-" + (levelIndex + 1) + " END+", LoadNextLevel);
            }
            else if (_strokeCount < par)
            {
                dialogueWrapper.StartDialogueSequence(_worldIndex + "-" + (levelIndex + 1) + " END-", LoadNextLevel);
            }
            else {
                dialogueWrapper.StartDialogueSequence(_worldIndex + "-" + (levelIndex + 1) + " END", LoadNextLevel);
            }
        } else
        {
            dialogueWrapper.StartDialogueSequence(_worldIndex + "-" + (levelIndex + 1) + " END", LoadNextLevel);
        }
    }

    private void StartNextLevelDialogue()
    {
        clubHandler.clubEnabled = false;
        clubHandler._clubHead.SetActive(false);
        dialogueWrapper.StartDialogueSequence(_worldIndex + "-" + (levelIndex + 1) + " INTRO", UnPauseGame);
    }

    private void UnPauseGame()
    {
        clubHandler.clubEnabled = true;
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelCoroutine());
    }

    private IEnumerator LoadNextLevelCoroutine()
    {
        if (updateCurrentLevelPositionToFloorHeightCoroutine != null) { StopCoroutine(updateCurrentLevelPositionToFloorHeightCoroutine); }
        UpdateParText();
        UpdateStrokeCountText();
        yield return AnimateOut();
        yield return AnimateIn();
        UpdateLastKnownBallPos();
        updateCurrentLevelPositionToFloorHeightCoroutine = StartCoroutine(UpdateCurrentLevelHeightToFloorHeight());
        UnPauseGame();
        StartNextLevelDialogue();
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
            currLevelHandler.LEVEL.transform.position = new Vector3(currLevelHandler.transform.position.x, worldFloor.transform.position.y, currLevelHandler.transform.position.z); //I dont know if the level handler is the actual thing that moves
            currLevelHandler.SetAnimateEndHeight(worldFloor.transform.position.y);
            yield return null;
        }
    }

    public void UpdateLevelPosition(Pose hitPose)
    {
        _ENVIRONMENT.transform.position = hitPose.position; //necessary to move level to where you tap in the world
        levelPosPose = hitPose;
        _mask.transform.position = new Vector3(_mask.transform.position.x ,-25 + hitPose.position.y, _mask.transform.position.z);
    }

    public void IncrementStrokeCount()
    {
        _strokeCount++;
        UpdateStrokeCountText();
    }

    private int CalculateScore()
    {
        return _strokeCount - JsonSerializer.Instance.golfPlayerData.WORLDS[_worldIndex].LEVELS[levelIndex].PAR;
    }

    public LevelHandler GetCurrLevelHandler()
    {
        return currLevelHandler;
    }

    public void ResetBallPosToLastKnownPos()
    {
        currLevelHandler.golfBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        currLevelHandler.golfBall.transform.position = lastKnownBallPos;
    }

    public void UpdateLastKnownBallPos()
    {
        lastKnownBallPos = currLevelHandler.golfBall.transform.position;
    }

    private void UpdateParText()
    {       
        parAnimationController.transformRelativeA(()=> {
            levelParText.text = "Par " + JsonSerializer.Instance.golfPlayerData.WORLDS[_worldIndex].LEVELS[levelIndex].PAR.ToString();
            parAnimationController.transformRelativeB(()=>{});

        });

        
    }

    private void UpdateStrokeCountText()
    {

        strokeAnimationController.scaleDown(() => {

            strokeCountText.text = _strokeCount.ToString();
            strokeAnimationController.scaleUp(()=> {});



        });
    }

    public DialogueWrapper GetDialogueWrapper()
    {
        return dialogueWrapper;
    }
}
