using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyor1 : MonoBehaviour
{

    void Update()

    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("GolfBallnon"))
        {


            other.gameObject.transform.Translate(new Vector3(0, 0, (float)-0.005), Space.World);
        }
    }
}
