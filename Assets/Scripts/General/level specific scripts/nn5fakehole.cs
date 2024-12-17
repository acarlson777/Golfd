using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nn5fakehole : MonoBehaviour
{
    readonly float x = GameObject.Find("GolfBallgen").transform.position.x;
    readonly float y = GameObject.Find("GolfBallgen").transform.position.y;
    readonly float z = GameObject.Find("GolfBallgen").transform.position.z;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            other.gameObject.transform.position = new Vector3(x,y + (float) 0.5,z); 
        }
        
        
    }
}
