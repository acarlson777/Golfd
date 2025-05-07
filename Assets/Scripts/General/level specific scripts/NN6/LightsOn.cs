using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
    public GameObject GolfBall;
    public GameObject Walls1a;
    public GameObject Ball_LightA;
    public GameObject Magical_Spot_LightC;
    //public GameObject LightA1; legacy
    //public GameObject LightA2; legacy
    public Material Glow;
    public Material white;
    public Material lights;
    public Material lightsOff;
    void Start()
    {
        Ball_LightA.SetActive(true);
        GolfBall.GetComponent<MeshRenderer>().material = Glow;
        Walls1a.GetComponent<MeshRenderer>().material = lightsOff;
        Magical_Spot_LightC.SetActive(false);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            GolfBall.GetComponent<MeshRenderer>().material = white;
            Walls1a.GetComponent<MeshRenderer>().material = lights;
            //LightA1.SetActive(false); legacy
            //LightA2.SetActive(false); legacy
            Magical_Spot_LightC.SetActive(true);
            Ball_LightA.SetActive(false);

        }
    }
}
