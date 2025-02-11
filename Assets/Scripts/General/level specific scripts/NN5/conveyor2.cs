using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyor2 : MonoBehaviour

{

    void Update()

    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("ball"))
        {


            other.gameObject.transform.Translate(new Vector3((float)0.03, (float)0.1, 0), Space.World);
        }
    }
}
