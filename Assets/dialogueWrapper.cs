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
    private bool isSequenceInProgress = false;
    private DialogueSequence currentSequence;

    private Action onSequenceComplete = null;

    // This method can be called externally to start a dialogue sequence by name
    public void StartDialogueSequence(string dialogueSequenceName, Action onDialogueComplete){

        if (isSequenceInProgress){
            Time.timeScale = 0;

            while (true)
            {
                Debug.LogError("STUPID!!!!!!!");
            }
        
        }
        else{

            isSequenceInProgress = true;

            onSequenceComplete = onDialogueComplete;

            DialogueSequence sequence = dialogueSequences.Find(seq => seq.sequenceName == dialogueSequenceName);

            if (sequence != null)
            {
                currentSequence = sequence;
                currentDialogueIndex = 0; // Reset the dialogue index for the new sequence
                showDialogueObjects();

            }
            else
            {
                Debug.LogError($"Dialogue sequence {dialogueSequenceName} not found.");
            }

        }
    }

    // This is the method that will be called when the player taps the screen
    public void Tap(InputAction.CallbackContext context)
    {
        if (context.performed && !isDialogueInProgress && isSequenceInProgress) // Check if the tap action was performed and no dialogue is currently in progress
        {
            StartDialogueFromList(); // Start the dialogue without a callback (since it's handled by OnDialogueComplete)
        }
    }

    void showDialogueObjects(){

        isDialogueInProgress = true;


        setPictures();



        // enable canvas 
        canvas.gameObject.SetActive(true);

        // use scaleUp on text box
        textBox.scaleUp(() =>{

            // Enable image 
            image1.gameObject.SetActive(true);
            image2.gameObject.SetActive(true);

            Debug.Log("Active is true"!);

            int completedCount = 0;
            
            Action onComplete = () => {
                completedCount++;
                if (completedCount >= 2){
                    StartDialogueFromList();
                }
            };

            image1.transformRelativeA(onComplete);
            image2.transformRelativeA(onComplete);

        });

       
        // use transfrm up on both image

    }


    void setPictures(){

        if (currentSequence.dialogueEntries.Count > 0) {
        // Find the first entry for Manager1 and Manager2
        DialogueSequence.TextboxDropdownEntry firstManager1Entry = null;
        DialogueSequence.TextboxDropdownEntry firstManager2Entry = null;

        // Iterate through the entries to find the first one for each manager
        foreach (var entry in currentSequence.dialogueEntries) {
            if (firstManager1Entry == null && entry.dropdownOption == DialogueSequence.TextboxDropdownEntry.DropdownOption.Manager1) {
                firstManager1Entry = entry;
            }
            if (firstManager2Entry == null && entry.dropdownOption == DialogueSequence.TextboxDropdownEntry.DropdownOption.Manager2) {
                firstManager2Entry = entry;
            }
            
            // Break early if both are found
            if (firstManager1Entry != null && firstManager2Entry != null) {
                break;
            }
        }

        // Now, set pictures for both managers if the first entries were found
        if (firstManager1Entry != null) {
            manager1.SetPictureForDialogue(firstManager1Entry.dialogueName);
        }
        if (firstManager2Entry != null) {
            manager2.SetPictureForDialogue(firstManager2Entry.dialogueName);
        }
        }
    }

    void hideDialogueObjects(){

        isDialogueInProgress = true;

        int completedCount = 0;
            
            Action onComplete = () => {
                completedCount++;
                if (completedCount >= 2){


                    image1.gameObject.SetActive(false);
                    image2.gameObject.SetActive(false);


                    textBox.scaleDown(() =>{

                        canvas.gameObject.SetActive(false);
                        isSequenceInProgress = false;
                        onSequenceComplete?.Invoke();




                    });



                }
            };

            image1.transformRelativeB(onComplete);
            image2.transformRelativeB(onComplete);


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
            hideDialogueObjects();

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