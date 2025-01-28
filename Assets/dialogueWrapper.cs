using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // Required for the new Input System

public class DialogueWrapper : MonoBehaviour{


    [SerializeField] Canvas canvas;
    [SerializeField] DialogueAnimationController textBox;
    [SerializeField] DialogueAnimationController image1;
    [SerializeField] DialogueAnimationController image2;
    [SerializeField] private List<DialogueSequence> dialogueSequences; // List of dialogue sequences

    [System.Serializable]
    public class DialogueSequence
    {
        public string sequenceName;  // The name of the dialogue sequence
        public List<TextboxDropdownEntry> dialogueEntries; // List of dialogues in this sequence

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
    }

    public DialogueManager manager1;
    public DialogueManager manager2;

    private int currentDialogueIndex = 0;
    private bool isDialogueInProgress = false;
    private DialogueSequence currentSequence;

    // This method can be called externally to start a dialogue sequence by name
    public void StartDialogueSequence(string dialogueSequenceName, Action onDialogueComplete){
        DialogueSequence sequence = dialogueSequences.Find(seq => seq.sequenceName == dialogueSequenceName);
        
        if (sequence != null)
        {
            currentSequence = sequence;
            currentDialogueIndex = 0; // Reset the dialogue index for the new sequence
            showDialogueObjects();
            StartDialogueFromList();
        }
        else
        {
            Debug.LogError($"Dialogue sequence {dialogueSequenceName} not found.");
        }
    }

    // This is the method that will be called when the player taps the screen
    public void Tap(InputAction.CallbackContext context)
    {
        if (context.performed && !isDialogueInProgress) // Check if the tap action was performed and no dialogue is currently in progress
        {
            StartDialogueFromList(); // Start the dialogue without a callback (since it's handled by OnDialogueComplete)
        }
    }

    void showDialogueObjects(){
        // TODO: aks manager 1 and manager 2 to set the picture before running 



        // enable canvas 
        canvas.gameObject.SetActive(true);

        // use scaleUp on text box 

        textBox.scaleUp(() =>{});

        // Enable image 
        // use transfrm up on both image

    }

    void hideDialogueObjects(){

    }

    void StartDialogueFromList(){
        // Ensure we haven't finished all dialogues in the sequence
        if (currentDialogueIndex < currentSequence.dialogueEntries.Count){
            // Get the current dialogue entry
            DialogueSequence.TextboxDropdownEntry currentEntry = currentSequence.dialogueEntries[currentDialogueIndex];

            // Choose the correct DialogueManager based on the dropdown option
            DialogueManager selectedManager = currentEntry.dropdownOption == DialogueSequence.TextboxDropdownEntry.DropdownOption.Manager1
                ? manager1
                : manager2;

            // Call StartDialogue with the appropriate dialogue name and callback to continue
            isDialogueInProgress = true;
            selectedManager.StartDialogue(currentEntry.dialogueName, OnDialogueComplete);
        }else
        {
            Debug.Log("All dialogues in the sequence are finished.");
            isDialogueInProgress = false; // Mark dialogue as complete
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
