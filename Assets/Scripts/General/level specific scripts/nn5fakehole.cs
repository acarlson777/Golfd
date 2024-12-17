using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nn5fakehole : MonoBehaviour
{
    GameObject b = GameObject.Find("GolfBallgen");
    //float x = GameObject.Find("GolfBallgen").transform.position.x;
    //float y = GameObject.Find("GolfBallgen").transform.position.y;
    //float z = GameObject.Find("GolfBallgen").transform.position.z;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            other.gameObject.transform.position = new Vector3(b.transform.position.x, b.transform.position.y + (float) 0.4, b.transform.position.z); 
        }
        
        
    }
}
