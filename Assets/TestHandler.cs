using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestHandler : MonoBehaviour{

    public DialogueManager manager;


    void Start(){


      manager.StartDialogue(onDialogueComplete);
      Debug.Log("Init from TestHandler");

    }


    public void onDialogueComplete(){



      Debug.Log("Dialogue finished in Handler");



    }
}
