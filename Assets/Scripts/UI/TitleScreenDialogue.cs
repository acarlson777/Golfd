using UnityEngine;
using System.Collections;

public class TitleScreenDialogue : MonoBehaviour
{
    [SerializeField] private DialogueWrapper dialogueWrapper;
    [SerializeField] bool resetFirstTimeStatus;

    void Start()
    {
        if (resetFirstTimeStatus)
        {
            PlayerPrefs.SetString("FirstTitleScreen", "true");
        }

        if (PlayerPrefs.GetString("FirstTitleScreen", "true").Equals("true"))
        {
            dialogueWrapper.StartDialogueSequence("INTRO-SEQUENCE", () => { });
        }
        PlayerPrefs.SetString("FirstTitleScreen", "false");
    }
}
