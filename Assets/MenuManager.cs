using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour{       

    public GameObject Menu;
    bool visible = false; 

    public void menuOn() {

        Menu.SetActive(true);
        visible = true;

    }

    public void menuOff(){

        Menu.SetActive(false);
        visible = false;

    }

    public void newScene(string sceneName){
        SceneHandler.Instance.LoadScene(sceneName);
    }
}

