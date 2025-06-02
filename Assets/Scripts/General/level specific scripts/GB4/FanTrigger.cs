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
        FanBox.SetActive(false);
    }
    

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("ball"))
        {
            FanOn = true;
            FanBox.SetActive(true);
            UnityEngine.Debug.Log("Fan is on");

        }
        else
        {
            FanOn = false;
            FanBox.SetActive(false);
        }
    }
}
