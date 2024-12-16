using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nn4fakehole : MonoBehaviour
{
    
    public void gen()
    {
        GameObject b;
        b = GameObject.Find("GolfBallgen");
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            other.gameObject.transform.position = new Vector3(0, 1, 0);
        }

    }
}
