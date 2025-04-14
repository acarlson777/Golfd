using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightsOutA : MonoBehaviour
{
    public GameObject GolfBall;
    public GameObject Ball_Light;
    public GameObject LightA1;
    public GameObject LightA2;
    public Material Glow;
    public Material Lit;
    void Start()
    {
        Ball_Light.SetActive(false);
        GolfBall.GetComponent<MeshRenderer>().material = Lit;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            GolfBall.GetComponent<MeshRenderer>().material = Glow;
            
            LightA1.SetActive(false);
            LightA2.SetActive(false);

        }
    }
}
