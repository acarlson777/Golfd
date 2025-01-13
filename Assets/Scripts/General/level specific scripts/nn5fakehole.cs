using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nn5fakehole : MonoBehaviour
{

   
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            other.gameObject.transform.position = new Vector3((float)0.3430001,(float)0.1906604,(float) -0.5275999); 
        }
        
        
    }
}
