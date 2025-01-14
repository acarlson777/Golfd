using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Required for the new Input System

public class dialogueWrapper : MonoBehaviour
{
    [SerializeField]
    private List<TextboxDropdownEntry> dialogueEntries;

    [System.Serializable]
    public class TextboxDropdownEntry
    {
        public string dialogueName;  // For the textbox input (string)
        public DropdownOption dropdownOption;  // Dropdown selection (enum with two options)

        public enum DropdownOption
        {
            Manager1,
            Manager2
        }
    }

    public DialogueManager manager1;
    public DialogueManager manager2;

    private int currentDialogueIndex = 0;
    private bool isDialogueInProgress = false;

    // This is the method that will be called when the player taps the screen
    public void Tap(InputAction.CallbackContext context)
    {
        if (context.performed && !isDialogueInProgress) // Check if the tap action was performed and no dialogue is currently in progress
        {
            StartDialogueFromList();
        }
    }

    void StartDialogueFromList()
    {
        // Ensure we haven't finished all dialogues
        if (currentDialogueIndex < dialogueEntries.Count)
        {
            // Get the current dialogue entry
            TextboxDropdownEntry currentEntry = dialogueEntries[currentDialogueIndex];

            // Choose the correct DialogueManager based on the dropdown option
            DialogueManager selectedManager = currentEntry.dropdownOption == TextboxDropdownEntry.DropdownOption.Manager1
                ? manager1
                : manager2;

            // Call StartDialogue with the appropriate dialogue name and callback to continue
            isDialogueInProgress = true;
            selectedManager.StartDialogue(currentEntry.dialogueName, OnDialogueComplete);
        }
    }

    void OnDialogueComplete()
    {
        // Dialogue is finished, move to the next dialogue
        currentDialogueIndex++;

        // Now the dialogue is no longer in progress, allowing the next dialogue to be triggered
        isDialogueInProgress = false;
    }
}
