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

    private bool canTapSFX = true;
    private bool canTapMUSIC = true;

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
        if (!canTapSFX)
        {
            return;
        }
        canTapSFX = false;
        sfx = PlayerPrefs.GetInt("sfx") == 1;
        mixer.SetFloat("sfxVol", sfx ? -80 : 0);
        sfx = !sfx;
        PlayerPrefs.SetInt("sfx", sfx ? 1 : 0);
        Debug.Log("internal sfx: " + sfx);
        StartCoroutine(WaitBeforeTapAgainSFX());
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
        if (!canTapMUSIC)
        {
            return;
        }
        canTapMUSIC = false;
        music = PlayerPrefs.GetInt("music") == 1;
        mixer.SetFloat("musicVol", music ? -80 : 0);
        music = !music;
        PlayerPrefs.SetInt("music", music ? 1 : 0);
        Debug.Log("internal music: " + music);
        StartCoroutine(WaitBeforeTapAgainMUSIC());

    }

    public bool getSfxState(){
        Debug.Log("SFX STATE: " + sfx);
        return sfx;
    } 
    public bool getMusicState(){
        Debug.Log("MUSIC STATE: " + music);
        return music;
    }

    public IEnumerator WaitBeforeTapAgainSFX()
    {
        yield return new WaitForSeconds(.3333f);
        canTapSFX = true;
    }

    public IEnumerator WaitBeforeTapAgainMUSIC()
    {
        yield return new WaitForSeconds(.3333f);
        canTapMUSIC = true;
    }
}