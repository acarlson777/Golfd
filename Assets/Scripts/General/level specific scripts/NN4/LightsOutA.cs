using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsOutA : MonoBehaviour
{
    public GameObject GolfBall;
    public GameObject Walls1;
    public GameObject Ball_Light;
    public GameObject Magical_purple_Spot_Lightb;
    //public GameObject LightA1; legacy
    //public GameObject LightA2; legacy
    public Material Glow;
    public Material Lit;
    public Material lights;
    void Start()
    {
        Ball_Light.SetActive(false);
        GolfBall.GetComponent<MeshRenderer>().material = Lit;
        Walls1.GetComponent<MeshRenderer>().material = lights;
        Magical_purple_Spot_Lightb.SetActive(true);
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            GolfBall.GetComponent<MeshRenderer>().material = Glow;
            Walls1.GetComponent<MeshRenderer>().material = Lit;
            //LightA1.SetActive(false); legacy
            //LightA2.SetActive(false); legacy
            Magical_purple_Spot_Lightb.SetActive(false);

        }
    }
}
