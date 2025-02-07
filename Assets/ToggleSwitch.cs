using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour{
    
    public Button onButton;
    public Button offButton;


    public void ON(){

        Debug.Log("clicked on");

        offButton.gameObject.SetActive(true);
        onButton.gameObject.SetActive(false);
        
    }


    public void OFF(){

        Debug.Log("clicked off");

        onButton.gameObject.SetActive(true);
        offButton.gameObject.SetActive(false);

    }
    
}
