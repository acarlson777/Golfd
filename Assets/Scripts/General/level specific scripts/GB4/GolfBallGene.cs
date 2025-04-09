using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallGene : MonoBehaviour
{
    public GameObject GolfBalConveyorEnt;
    public GameObject GolfBallnon;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "GolfBallnon")
        {
            // teleports the Golf ball to the Golf Ball generator
            GolfBallnon.transform.position = GolfBalConveyorEnt.transform.position;

        }
    }
}
