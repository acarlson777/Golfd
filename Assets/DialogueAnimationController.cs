using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimationController : MonoBehaviour{

    [SerializeField] float easeInDuration;
    [SerializeField] float easeOutDuration;
    [SerializeField] LeanTweenType easeInType;
    [SerializeField] LeanTweenType easeOutType;


    public void Start(){

        scaleUp(() =>
            {
                Debug.Log("FINIISHED SCALE UP");
            });

    }


    public void scaleUp(Action callback){

    LeanTween.scale(gameObject, new Vector3(1, 1, 1), easeInDuration)
             .setEase(easeInType)
             .setOnComplete(() => callback?.Invoke());
             
    }

    
    public void scaleDown(Action callback){

    LeanTween.scale(gameObject, new Vector3(0.01f, 0.01f, 0.01f), easeOutDuration)
             .setEase(easeOutType)
             .setOnComplete(() => callback?.Invoke());
             
    }


    void transformUp(){


    }




}



