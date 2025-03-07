using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour{

    [SerializeField] float easeInDuration;
    [SerializeField] float easeOutDuration;
    [SerializeField] LeanTweenType easeInType;
    [SerializeField] LeanTweenType easeOutType;

    [SerializeField] Vector2 positionA;

    [SerializeField] Vector2 positionB;

    private Vector2 initPosition;


    public float amplitude = 10f;
    public float frequency = 1f;  

    private RectTransform rectTransform;
    
    private int tweenID;

    public bool cancel = false;


    public void Awake(){


        rectTransform = GetComponent<RectTransform>();
        initPosition = rectTransform.anchoredPosition;
        print(initPosition);
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

    public void transformAbsoluteA(Action callback){


        LeanTween.moveLocal(gameObject, positionA, easeInDuration)
                 .setEase(easeInType)
                 .setOnComplete(() => callback?.Invoke());

    }

    public void transformAbsoluteB(Action callback){


        LeanTween.moveLocal(gameObject, positionB, easeOutDuration)
                 .setEase(easeOutType)
                 .setOnComplete(() => callback?.Invoke());

    }


    public void transformRelativeA(Action callback){




        LeanTween.move(rectTransform, (new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y) + positionA), easeInDuration)
                 .setEase(easeInType)
                 .setOnComplete(() =>callback?.Invoke());

    }

    public void transformRelativeB(Action callback){


     


        LeanTween.move(rectTransform, (new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y) + positionB), easeOutDuration)
                 .setEase(easeOutType)
                 .setOnComplete(() => callback?.Invoke());

    }


    public void startSinAnimation(){


        cancel = false;
        
        initPosition = rectTransform.anchoredPosition;

        tweenID = LeanTween.value(gameObject, UpdateYPosition, 0f, Mathf.PI * 2f, frequency)
            .setLoopClamp() 
            .setRepeat(-1)
            .id; 
    }

    void UpdateYPosition(float t){

        float sineValue = Mathf.Sin(t); 

        float yPosition = amplitude * sineValue + initPosition.y; 


        //Debug.Log(initPosition.y);
        //Debug.Log(yPosition);


        if(cancel && (Math.Abs(initPosition.y - yPosition) < 0.5f)){
            stopSinAnimation(false);
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, yPosition);
    }

    public void stopAllAnimation(){
         LeanTween.cancel(gameObject, tweenID);
    }

    public void stopSinAnimation(bool teleport){

        if(teleport){

            LeanTween.cancel(gameObject, tweenID);
            rectTransform.anchoredPosition = initPosition;
            return;

        }

        // set request stop to true

         if(cancel){
            LeanTween.cancel(gameObject, tweenID);
            rectTransform.anchoredPosition = initPosition;

            cancel = false;
        }else{
            cancel = true;
        }

        


       


        
    }





        //gameObject.GetComponent<RectTransform>().anchoredPosition = LeanTween.value(gameObject, currentPos, currentPos + new Vector2(100f,100f), 10).setEase(easeInType);

        //LeanTween.move(gameObject, positionA, easeInDuration);
        //LeanTween.(gameObject, transform.position.y + 100, 1f)
            //  .setEase(easeInType);
            //  .setOnComplete(() => callback?.Invoke());






}



