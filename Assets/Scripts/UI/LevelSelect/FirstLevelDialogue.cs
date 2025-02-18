using UnityEngine;
using System.Collections;

public class FirstLevelDialogue : MonoBehaviour
{
    [SerializeField] private DialogueWrapper dialogueWrapper;
    [SerializeField] bool resetFirstTimeStatus;
    [SerializeField] LevelPlacementHandler levelPlacementHandler;

    void Start()
    {
        if (resetFirstTimeStatus)
        {
            PlayerPrefs.SetString("isFirstTimeInLevel", "true");
        }

        if (PlayerPrefs.GetString("isFirstTimeInLevel", "true").Equals("true"))
        {
            levelPlacementHandler.canPlaceLevel = false;
            dialogueWrapper.StartDialogueSequence("INTRO-SEQUENCE", () => {levelPlacementHandler.canPlaceLevel = true; });
        }
        PlayerPrefs.SetString("isFirstTimeInLevel", "false");
    }
}

