using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskAllChildren : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<MeshRenderer>() == null ) { continue; }
            child.gameObject.AddComponent(typeof(ObjectToBeMasked));
        }
    }
}
