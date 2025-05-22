using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOn : MonoBehaviour
{
    [SerializeField] private Light levelLight;

    void Start()
    {

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            levelLight.intensity = 3;
        }
    }
}
