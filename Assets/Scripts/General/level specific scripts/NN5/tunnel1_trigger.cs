using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnel1_trigger : MonoBehaviour
{

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("ball"))
        {
            other.gameObject.transform.position = new Vector3(6, (float)-5, (float)-69);
        }

    }
}
