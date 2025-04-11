using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallGene : MonoBehaviour
{
    public GameObject GolfBalConveyorEnt;
    

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GolfBallnon")
        {
            // teleports the Golf ball to the Golf Ball generator
            other.transform.position = GolfBalConveyorEnt.transform.position;

        }
    }
}
