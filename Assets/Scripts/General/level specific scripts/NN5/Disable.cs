using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disable: MonoBehaviour
{
    public GameObject GolfBall;
    public GameObject Forcefield;
    
  
    void Start()
    {
        

    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            
            Forcefield.SetActive(false);
            
        }
    }
}
