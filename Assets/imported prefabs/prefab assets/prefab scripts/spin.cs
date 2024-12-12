using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidtrigger : MonoBehaviour
{

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("ball"))
        {
            other.gameObject.transform.position = new Vector3(0, 1, 0);
        }

    }
}
