using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandler : MonoBehaviour{

    public DialogueManager manager;


    void Awake(){

      Debug.Log("Init from TestHandler");
      Debug.Log("Init from TestHandler");
      Debug.Log("Init from TestHandler");
      Debug.Log("Init from TestHandler");
      Debug.Log("Init from TestHandler");
      Debug.Log("Init from TestHandler");
      Debug.Log("Init from TestHandler");


      StartCoroutine(PauseExecution());


      manager.StartDialogue("test123", onDialogueComplete);


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
        // Continue with the rest of your logic here
    }
}
