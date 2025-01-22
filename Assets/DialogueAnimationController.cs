using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimationController : MonoBehaviour{

    [SerializeField] float duration;
    [SerializeField] LeanTweenType easeType;


    public void Start(){

        scaleUp();

    }


    void scaleUp(){



        LeanTween.scale(gameObject, new Vector3(1, 1, 1), duration).setEase(easeType).setOnComplete(wait);

        

    }


    void wait(){

        LeanTween.scale(gameObject, transform.localScale, 2).setOnComplete(scaleDown);

    }


    void wait1(){

        LeanTween.scale(gameObject, transform.localScale, 2).setOnComplete(scaleUp);

    }



    void scaleDown(){


       

        LeanTween.scale(gameObject, new Vector3(0.01f,0.01f,0.01f), 0).setOnComplete(wait1);

        
    }



    void transformUp(){


    }




}



