using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : MonoBehaviour
{
    public GameObject GolfBall;
    public GameObject Spot_LightA1;
    public GameObject Spot_LightA2;
    public GameObject Spot_LightA3;
    public GameObject ForcefieldA1;
    public GameObject Golf_light;
    void Start()
    {
        Golf_light.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            Spot_LightA1.SetActive(false);
            Spot_LightA2.SetActive(false);
            Spot_LightA3.SetActive(false);
            ForcefieldA1.SetActive(false);
            Golf_light.SetActive(true);
        }
    }
}
