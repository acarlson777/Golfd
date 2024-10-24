using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio; 

public class SoundHandler : MonoBehaviour {



    public AudioMixer sfxMixer; 
    public AudioMixer musicMixer; 


    private bool sfx = true;
    private bool music = true;




    public void TapSfx(InputAction.CallbackContext context) {


        if (context.started){


            sfxMixer.SetFloat("masterVol", sfx ? -80 : 0);
            sfx = !sfx;


            Debug.Log("SFX volume changed");

        }

    }


    public void TapMusic(InputAction.CallbackContext context) {

        if (context.started){

            musicMixer.SetFloat("masterVol", music ? -80 : 0);
            music = !music;

        }

    }

    
}
