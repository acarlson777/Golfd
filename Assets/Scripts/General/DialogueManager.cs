using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void StartDialogue(string[] lines, Action onDialogueComplete){

        EndDialogue(onDialogueComplete);
    }

    private void EndDialogue(Action onDialogueComplete){

        onDialogueComplete?.Invoke();
    }
}
