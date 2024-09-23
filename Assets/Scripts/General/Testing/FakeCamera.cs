using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;

    void Update()
    {
        //transform.position += (camera.transform.position - transform.position + offset) / convergeConstant;
        transform.position = camera.transform.position;
        transform.rotation = camera.transform.rotation;
    }
}
