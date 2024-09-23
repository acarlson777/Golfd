using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderChecker : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        print("It has been touched!");
    }
}
