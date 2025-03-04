using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonAnimationController : MonoBehaviour{

    private SoundHandler soundHandler;
    public Button buttonSfx;
    private Animator sfxAnimator;
    public Button buttonMusic;
    private Animator musicAnimator;

    private bool sfx;
    private bool music;

    void OnEnable(){

        if(buttonMusic == null || buttonSfx == null){
            // Error message
            return;
        }

        soundHandler = GameObject.FindGameObjectWithTag("SoundHandler").GetComponent<SoundHandler>();
        if (soundHandler == null){
            return;
        }

        sfxAnimator = buttonSfx.GetComponent<Animator>();
        musicAnimator = buttonMusic.GetComponent<Animator>();

        sfx = PlayerPrefs.GetInt("sfx") == 1;
        music = PlayerPrefs.GetInt("music") == 1;

        sfxAnimator.SetBool("isOn", sfx);
        musicAnimator.SetBool("isOn", music);

        buttonSfx.onClick.AddListener(soundHandler.TapSfxButton);
        buttonMusic.onClick.AddListener(soundHandler.TapMusicButton);
    }

    public void OnSfxButtonClick(){
        sfx = !sfx;
        sfxAnimator.SetBool("isOn", sfx);
        sfxAnimator.SetTrigger("isAnimating");
        // soundHandler.TapSfxButton();
        Debug.Log("called sfx sound handler and set to " + sfx);
    }

    public void OnMusicButtonClick(){
        music = !music;
        musicAnimator.SetBool("isOn", music);
        musicAnimator.SetTrigger("isAnimating");
        // soundHandler.TapMusicButton();
        Debug.Log("called music sound handler and set to " + music);
    }
}
