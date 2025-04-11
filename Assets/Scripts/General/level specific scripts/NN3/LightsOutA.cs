using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOutA : MonoBehaviour
{
    public GameObject Ball_Light;
    public GameObject LightA1;
    public GameObject LightA2;

    void Start()
    {
        Ball_Light.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            LightA1.SetActive(false);
            LightA2.SetActive(false);
        }
    }
}
