using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadowButton : MonoBehaviour
{

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            BroadcastMessage("lightsoff");
            
        }

    }
}
