using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour{



    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public List<string> dialogueLines;
    public float typingSpeed = 0.05f;
    private int currentLineIndex = -1;
    private Action onDialogueComplete;
    private Coroutine typingCoroutine;



    void Start(){



    }

    public void Tap() {
        if (currentLineIndex >= 0) {
            if (typingCoroutine != null) {
                // Stop the typing coroutine and show the full text
                StopCoroutine(typingCoroutine);
                dialogueText.text = dialogueLines[currentLineIndex];
                typingCoroutine = null; // Reset the coroutine reference
            } else if (dialogueText.text == dialogueLines[currentLineIndex]) {
                // Proceed to the next line if the current line is fully displayed
                NextLine();
            }
        }
    }



    public void StartDialogue(Action onDialogueComplete){



      this.onDialogueComplete = onDialogueComplete;

      currentLineIndex = 0;
      ShowLine(dialogueLines[currentLineIndex]);

    }

    private void EndDialogue(){

        Debug.Log("Dialoge Finished");

        onDialogueComplete?.Invoke();
    }


    private void ShowLine(string line){

      dialogueText.text = "";
      typingCoroutine = StartCoroutine(TypeLine(line));

    }


    public void NextLine(){
        if (currentLineIndex < dialogueLines.Count - 1){
            currentLineIndex++;
            ShowLine(dialogueLines[currentLineIndex]);
        }
        else{
            EndDialogue();
        }
    }


    private IEnumerator TypeLine(string line){

      foreach (char letter in line){
        dialogueText.text += letter;
        yield return new WaitForSeconds(typingSpeed);
      }
      typingCoroutine = null; 

    }
}
