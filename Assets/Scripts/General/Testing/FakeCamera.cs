using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float convergeConstant;

    void Update()
    {
        transform.position += (camera.transform.position - transform.position) / convergeConstant;
        transform.rotation = camera.transform.rotation;
    }
}
