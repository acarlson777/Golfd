using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class voidTrigger : MonoBehaviour
{

    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("GolfBall"))
        {
            other.gameObject.transform.position = new Vector3(0, 1, 0);
        }

    }
}
