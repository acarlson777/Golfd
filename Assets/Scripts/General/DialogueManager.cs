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
    private int currentLineIndex = 0;
    private Action onDialogueComplete;



    void Start(){

    }

    void Update(){

        if (Input.GetMouseButtonDown(0)){
            if (dialogueText.text != dialogueLines[currentLineIndex]){

                dialogueText.text = dialogueLines[currentLineIndex];
            }
            else{
                NextLine();
            }
        }

    }



    public void StartDialogue(string[] lines, Action onDialogueComplete){



      this.onDialogueComplete = onDialogueComplete;

      currentLineIndex = 0;
      ShowLine(dialogueLines[currentLineIndex]);

    }

    private void EndDialogue(){

        onDialogueComplete?.Invoke();
    }


    private void ShowLine(string line){

      dialogueText.text = "";
      StartCoroutine(TypeLine(line));

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

    }
}
