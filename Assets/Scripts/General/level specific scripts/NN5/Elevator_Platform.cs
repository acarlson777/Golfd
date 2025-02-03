using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Platform : MonoBehaviour
{

    void Update()

    {

    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("GolfBall"))
        {


            transform.Translate(new Vector3(0, (float)0.01, 0), Space.World);
        }
    }
}
