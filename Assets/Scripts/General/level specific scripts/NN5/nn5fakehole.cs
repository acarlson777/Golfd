using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nn5fakehole : MonoBehaviour
{
    public GameObject GolfBallGen;
    public GameObject GolfBall;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            // teleports the Golf ball to the Golf Ball generator
            GolfBall.transform.position = GolfBallGen.transform.position;
            
        }
    }
}
