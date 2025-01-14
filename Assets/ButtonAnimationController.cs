using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ButtonAnimationController : MonoBehaviour{

    public SoundHandler soundHandler;
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

        if(soundHandler == null){
            return;
        }

        // get the state of the button 
        sfx = soundHandler.getSfxState();
        music = soundHandler.getMusicState();

        Debug.Log("sfx: " + sfx);
        Debug.Log("music: " + music);


        sfxAnimator = buttonSfx.GetComponent<Animator>();
        musicAnimator = buttonMusic.GetComponent<Animator>();

        sfxAnimator.SetBool("isOn", sfx);
        musicAnimator.SetBool("isOn", music);



        buttonSfx.onClick.AddListener(onSfxButtonClick);
        buttonMusic.onClick.AddListener(OnMusicButtonClick);
        

    }

    public void onSfxButtonClick(){


            sfx = !sfx;
            sfxAnimator.SetBool("isOn", sfx);
            // soundHandler.TapSfxButton();
            Debug.Log("called sound handler!");

         

    }

    public void OnMusicButtonClick(){



            music = !music;
            musicAnimator.SetBool("isOn", music);
            // soundHandler.TapMusicButton();
            Debug.Log("called sound handler!");

         
    }
}
