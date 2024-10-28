using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio; 

public class SoundHandler : MonoBehaviour {

    public static SoundHandler Instance { get; private set; }

    public AudioMixer mixer;

    private bool sfx = true;
    private bool music = true;

    private void Awake() {
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void TapSfx(InputAction.CallbackContext context) {


        if (context.started){


            mixer.SetFloat("sfxVol", sfx ? -80 : 0);
            sfx = !sfx;
        }
    }

    public void TapMusic(InputAction.CallbackContext context) {

        if (context.started){

            mixer.SetFloat("musicVol", music ? -80 : 0);
            music = !music;
        }
    }
}