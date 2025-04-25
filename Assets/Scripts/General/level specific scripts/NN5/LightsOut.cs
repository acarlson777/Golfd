using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : MonoBehaviour
{
    public GameObject GolfBall;
    public GameObject lightbulb;
    public GameObject lightbulb1;
    public GameObject lightbulb2;
    public GameObject Spot_LightA1;
    public GameObject Spot_LightA2;
    public GameObject Spot_LightA3;
    public GameObject ForcefieldA1;
    public GameObject Golf_light;
    public Material Glow;
    public Material Lit;
    public Material lights;
    public Material lightsOff;
    void Start()
    {
        Golf_light.SetActive(false);
        GolfBall.GetComponent<MeshRenderer>().material = Lit;
        lightbulb.GetComponent<MeshRenderer>().material = lights;
        lightbulb1.GetComponent<MeshRenderer>().material = lights;
        lightbulb2.GetComponent<MeshRenderer>().material = lights;

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            GolfBall.GetComponent<MeshRenderer>().material = Glow;
            lightbulb.GetComponent<MeshRenderer>().material = lightsOff;
            lightbulb1.GetComponent<MeshRenderer>().material = lightsOff;
            lightbulb2.GetComponent<MeshRenderer>().material = lightsOff;
            Spot_LightA1.SetActive(false);
            Spot_LightA2.SetActive(false);
            Spot_LightA3.SetActive(false);
            ForcefieldA1.SetActive(false);
            Golf_light.SetActive(true);
        }
    }
}
