using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinbutx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3((float)0.3, 0, 0), Space.Self);
    }
}
