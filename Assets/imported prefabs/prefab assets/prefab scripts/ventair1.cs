using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ventair1 : MonoBehaviour
{

    void Update()

    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name.Equals("ball"))
        {


            other.gameObject.transform.Translate(new Vector3(0, (float)0.2, 0), Space.World);
        }
    }
}
