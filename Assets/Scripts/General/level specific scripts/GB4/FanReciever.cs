using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanReciever : MonoBehaviour
{
    public FanTrigger FanOn;
    public GameObject FanBox;


    // Start is called before the first frame update
    void Start()
    {
        FanBox.GetComponent<BoxCollider>();
        
    }

    void OnTriggerStay(Collider other)
    {
        if (FanOn == true)
        {
            {
                other.gameObject.transform.Translate(new Vector3((float)0.03, (float)0.1, 0), Space.World);
                UnityEngine.Debug.Log("Fan is on");
            }
        }
    }
}
