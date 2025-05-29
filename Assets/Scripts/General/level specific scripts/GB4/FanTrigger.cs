using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanTrigger : MonoBehaviour
{
    public GameObject FanBox;
    public GameObject GolfBall;
    public bool FanOn;

    void Start()
    {
        FanOn = false;
    }
    

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("ball"))
        {
            FanOn = true;

            
        }
        else
        {
            FanOn = false;
        }
    }
}
