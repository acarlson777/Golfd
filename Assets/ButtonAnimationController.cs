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

    private bool canTapSFX = true;
    private bool canTapMUSIC = true;

    private void Start()
    {
   
    }

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

        print(sfx);
        print(music);

        sfxAnimator.SetBool("isOn", sfx);
        musicAnimator.SetBool("isOn", music);
        canTapSFX = true;
        canTapMUSIC = true;

        buttonSfx.onClick.AddListener(soundHandler.TapSfxButton);
        buttonMusic.onClick.AddListener(soundHandler.TapMusicButton);
    }

    public void OnSfxButtonClick(){
        if (!canTapSFX)
        {
            return;
        }
        sfx = !sfx;
        sfxAnimator.SetBool("isOn", sfx);
        sfxAnimator.SetTrigger("isAnimating");
        canTapSFX = false;
        StartCoroutine(WaitBeforeTapAgainSFX());

        // soundHandler.TapSfxButton();
        Debug.Log("called sfx sound handler and set to " + sfx);
    }

    public void OnMusicButtonClick(){
        if (!canTapMUSIC)
        {
            return;
        }
        music = !music;
        musicAnimator.SetBool("isOn", music);
        musicAnimator.SetTrigger("isAnimating");
        canTapMUSIC = false;
        StartCoroutine(WaitBeforeTapAgainMUSIC());

        // soundHandler.TapMusicButton();
        Debug.Log("called music sound handler and set to " + music);
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
