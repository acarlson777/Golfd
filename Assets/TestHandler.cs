using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandler : MonoBehaviour{

    public DialogueManager manager;


    void Start(){


      StartCoroutine(PauseExecution());


    }


    public void onDialogueComplete(){



      Debug.Log("Dialogue finished in Handler");



    }



    private IEnumerator PauseExecution()
    {
        Debug.Log("Execution paused for 2 seconds.");

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        Debug.Log("Execution resumed.");
        manager.StartDialogue("test1234", onDialogueComplete);
    }
}