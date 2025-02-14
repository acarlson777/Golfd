using UnityEngine;
using System.Collections;

public class FirstLevelDialogue : MonoBehaviour
{
    [SerializeField] private DialogueWrapper dialogueWrapper;
    [SerializeField] bool resetFirstTimeStatus;

    void Start()
    {
        if (resetFirstTimeStatus)
        {
            PlayerPrefs.SetString("isFirstTimeInLevel", "true");
        }

        if (PlayerPrefs.GetString("isFirstTimeInLevel", "true").Equals("true"))
        {
            dialogueWrapper.StartDialogueSequence("INTRO-SEQUENCE", () => { });
        }
        PlayerPrefs.SetString("isFirstTimeInLevel", "false");
    }
}

