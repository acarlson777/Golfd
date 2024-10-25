using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Audio; 

public class SoundHandler : MonoBehaviour {



    public AudioMixer mixer;

    private bool sfx = true;
    private bool music = true;




    public void TapSfx(InputAction.CallbackContext context) {


        if (context.started){


            mixer.SetFloat("sfxVol", sfx ? -80 : 0);
            sfx = !sfx;


            Debug.Log("SFX volume changed");

        }

    }


    public void TapMusic(InputAction.CallbackContext context) {

        if (context.started){

            mixer.SetFloat("musicVol", music ? -80 : 0);
            music = !music;

        }

    }

    
}
