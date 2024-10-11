using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.05f;


    [SerializeField]
    private List<DialogueEntry> dialogueEntries;

    private Dictionary<string, List<string>> dialogueDict;
    private int currentLineIndex = -1;
    private Action onDialogueComplete;
    private Coroutine typingCoroutine;

    [System.Serializable]
    public class DialogueEntry{
        public string name;
        public List<string> lines;
    }

    void Start(){
        Debug.Log("STAZRT");
        dialogueDict = new Dictionary<string, List<string>>();

        foreach (var entry in dialogueEntries){
            dialogueDict[entry.name] = entry.lines;
        }



        foreach (var kvp in dialogueDict){
            Debug.Log($"Key: {kvp.Key}, Lines Count: {kvp.Value.Count}");
        }
    }

    public void Tap(InputAction.CallbackContext context){

      Debug.Log("click");
        if (context.started){
            if (currentLineIndex >= 0){
                if (typingCoroutine != null){
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = dialogueDict[dialogueEntries[0].name][currentLineIndex];
                    typingCoroutine = null; //
                }
                else if (dialogueText.text == dialogueDict[dialogueEntries[0].name][currentLineIndex]){

                    NextLine();
                }
            }
        }
    }

    public void StartDialogue(string dialogueName, Action onDialogueComplete){


        if (dialogueDict.TryGetValue(dialogueName, out List<string> lines)){
            this.onDialogueComplete = onDialogueComplete;
            currentLineIndex = 0;

            ShowLine(lines[currentLineIndex]);
        }else{

            Debug.LogWarning($"Dialogue '{dialogueName}' not found!");
        }
    }

    private void EndDialogue(){
        Debug.Log("Dialogue Finished");
        onDialogueComplete?.Invoke();
      }

    private void ShowLine(string line){
        dialogueText.text = "";
        typingCoroutine = StartCoroutine(TypeLine(line));
    }

    public void NextLine(){
        if (currentLineIndex < dialogueDict[dialogueEntries[0].name].Count - 1){
            currentLineIndex++;
            ShowLine(dialogueDict[dialogueEntries[0].name][currentLineIndex]);

        }else{
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
