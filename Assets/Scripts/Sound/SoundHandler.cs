using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio; 
using System;

public class SoundHandler : MonoBehaviour {

    public static SoundHandler Instance { get; private set; }

    public AudioMixer mixer;

    private bool sfx;
    private bool music;

    private void Awake() {
        if (Instance != null && Instance != this){
            Destroy(this);
        }
        else{
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    /*
    public void TapSfx(InputAction.CallbackContext context){
        if (context.started){
            ToggleSfx();
        }
    }
    */

    public void TapSfxButton(){
        ToggleSfx();
    }

    private void ToggleSfx(){
        sfx = PlayerPrefs.GetInt("sfx") == 1;
        mixer.SetFloat("sfxVol", sfx ? -80 : 0);
        sfx = !sfx;
        PlayerPrefs.SetInt("sfx", sfx ? 1 : 0);
        Debug.Log("internal sfx: " + sfx);
    }

    /*
    public void TapMusic(InputAction.CallbackContext context) {

        if (context.started){
            ToogleMusic();
        }
    }
    */

    public void TapMusicButton(){
        ToogleMusic();
    }

    private void ToogleMusic(){
        music = PlayerPrefs.GetInt("music") == 1;
        mixer.SetFloat("musicVol", music ? -80 : 0);
        music = !music;
        PlayerPrefs.SetInt("music", music ? 1 : 0);
        Debug.Log("internal music: " + music);
    }

    public bool getSfxState(){
        Debug.Log("SFX STATE: " + sfx);
        return sfx;
    } 
    public bool getMusicState(){
        Debug.Log("MUSIC STATE: " + music);
        return music;
    }
}