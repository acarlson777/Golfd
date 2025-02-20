using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionUpdater : MonoBehaviour{


    // TODO: access galfball from worldHandler, level handler,


    public GameObject target;
    public string name;  


    public Vector3 offset; 

    private float yPosition = 0f;

    public float amplitude = 10f;
    public float frequency = 1f;  
    private int tweenID;


    void Awake(){
        if(target == null && (GameObject.FindGameObjectWithTag(name) != null)) {
            target = GameObject.FindGameObjectWithTag(name);
        }

        // startSinAnimation();
    }



    void Update(){

        if (target != null){
            transform.position = new Vector3(target.transform.position.x, target.transform.position.y + yPosition, target.transform.position.z) + offset;
        }
        
    }



    void startSinAnimation(){

        tweenID = LeanTween.value(gameObject, UpdateYPosition, 0f, Mathf.PI * 2f, frequency)
            .setLoopClamp() 
            .setRepeat(-1)
            .id; 
    }

    void UpdateYPosition(float t){

        float sineValue = Mathf.Sin(t); 

        float yPosition = amplitude * sineValue;

        Debug.Log(yPosition);
    }

}


    // public void stopAllAnimation(){
    //      LeanTween.cancel(gameObject, tweenID);
    // }