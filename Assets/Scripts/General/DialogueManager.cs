using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Audio; 

public class DialogueManager : MonoBehaviour {
    public Image characterImage;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterName;
    public float typingSpeed = 0.05f;

    [SerializeField]
    private List<DialogueEntry> dialogueEntries;

    public AudioMixer audioMixer; 

    public DialogueAnimationController animator;
    private Dictionary<string, List<string>> dialogueDict;
    private Dictionary<string, List<string>> namesDict;
    private Dictionary<string, List<Sprite>> imagesDict;
    private Dictionary<string, List<AudioClip>> audioDict;
    private int currentLineIndex = -1;
    private Action onDialogueComplete;
    private Coroutine typingCoroutine;

    private string currentDialogueName;
    private AudioSource audioSource;

    [System.Serializable]
    public class DialogueEntry {
        public string name;
        public List<string> lines;
        public List<string> names;
        public List<Sprite> images;
        public List<AudioClip> audioClips;
    }

    void Start() {
        dialogueDict = new Dictionary<string, List<string>>();
        namesDict = new Dictionary<string, List<string>>();
        imagesDict = new Dictionary<string, List<Sprite>>();
        audioDict = new Dictionary<string, List<AudioClip>>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("SFX")[0];  // Set output to the SFX group

        foreach (var entry in dialogueEntries) {
            if ((entry.lines.Count == entry.names.Count) && 
                (entry.lines.Count == entry.images.Count) &&
                (entry.lines.Count == entry.audioClips.Count)) {
                dialogueDict[entry.name] = entry.lines;
                namesDict[entry.name] = entry.names;
                imagesDict[entry.name] = entry.images;
                audioDict[entry.name] = entry.audioClips;
            } else {
                Debug.LogError($"Dialogue entry {entry.name} components not equal in size! Dialogue will not work!");
            }
        }
    }

    public void Tap(InputAction.CallbackContext context) {
        if (context.started) {
            if (currentLineIndex >= 0) {
                if (typingCoroutine != null) {
                    StopCoroutine(typingCoroutine);
                    dialogueText.text = dialogueDict[currentDialogueName][currentLineIndex];
                    typingCoroutine = null;
                    // Stop the current audio 
                    audioSource.Stop();
                    // Stop the animation 
                    animator.startSinAnimation();
                } else if (dialogueText.text == dialogueDict[currentDialogueName][currentLineIndex]) {
                    NextLine();
                }
            }
        }
    }

    public void StartDialogue(string dialogueName, Action onDialogueComplete) {
        currentDialogueName = dialogueName;

        if (dialogueDict.TryGetValue(dialogueName, out List<string> lines)) {
            this.onDialogueComplete = onDialogueComplete;
            currentLineIndex = 0;

            ShowLine(lines[currentLineIndex]);
        } else {
            Debug.LogWarning($"Dialogue '{dialogueName}' not found!");
        }
    }

    private void EndDialogue() {
        Debug.Log("Dialogue Finished");
        currentLineIndex = -1;
        currentDialogueName = "";
        onDialogueComplete?.Invoke();
    }

    private void ShowLine(string line) {
        dialogueText.text = "";
        characterImage.sprite = imagesDict[currentDialogueName][currentLineIndex];
        characterName.text = namesDict[currentDialogueName][currentLineIndex];
        typingCoroutine = StartCoroutine(TypeLine(line));
        PlayAudioClip();
        StartAnimation();
    }

    public void NextLine() {
        if (currentLineIndex < dialogueDict[currentDialogueName].Count - 1) {
            currentLineIndex++;
            ShowLine(dialogueDict[currentDialogueName][currentLineIndex]);
        } else {
            EndDialogue();
        }
    }

    private IEnumerator TypeLine(string line) {
        foreach (char letter in line) {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingCoroutine = null;
        // Stop the audio
        audioSource.Stop();
        // Stop the animation
        animator.stopSinAnimation();
        
    }

    private void PlayAudioClip() {
        if (audioDict[currentDialogueName].Count > currentLineIndex) {
            AudioClip clip = audioDict[currentDialogueName][currentLineIndex];
            if (clip != null) {  
                audioSource.clip = clip;
                audioSource.Play();
            } else {
                Debug.LogWarning($"No audio clip assigned for {currentDialogueName} at line {currentLineIndex}.");
            }
        }
    }

    private void StartAnimation(){


        animator.startSinAnimation();

    }
}
