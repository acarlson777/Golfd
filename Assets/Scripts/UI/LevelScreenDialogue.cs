using UnityEngine;
using System.Collections;

public class LevelScreenDialogue : MonoBehaviour
{
    [SerializeField] private DialogueWrapper dialogueWrapper;
    [SerializeField] bool resetFirstTimeStatus;

    void Start()
    {
        if (resetFirstTimeStatus)
        {
            PlayerPrefs.SetString("FirstLevelScreen", "true");
        }

        if (PlayerPrefs.GetString("FirstLevelScreen", "true").Equals("true"))
        {
            dialogueWrapper.StartDialogueSequence("INTRO-SEQUENCE", () => { });
        }
        PlayerPrefs.SetString("FirstLevelScreen", "false");
    }
}
