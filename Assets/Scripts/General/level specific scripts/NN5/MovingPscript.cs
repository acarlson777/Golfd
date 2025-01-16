using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MovingPscript : MonoBehaviour
{
    // Start is called before the first frame update
    public float xint;
    public bool plusfour;
    public GameObject ObjectObstacle;
   
    void Start()
    {
        xint = 0;
        plusfour = false;
        xint = ObjectObstacle.transform.position.x;
    }

    
    
    
    // Update is called once per frame
    void Update()

    {
        StartCoroutine(sleepCoroutine());
        
    }

    private IEnumerator sleepCoroutine()
    {

        yield return new WaitForSeconds(5);
        Debug.Log("Waiting");
        if (xint > 4)
        {
            plusfour = true;
            Debug.Log("True");
        }
        if (xint < 1)
        {
            plusfour = false;
            Debug.Log("false");
        }
        if (plusfour == false)
        {
            transform.Translate(new Vector3((float)0.05, 0, 0), Space.World);
        }
        if (plusfour == true)
        {
            transform.Translate(new Vector3((float)-0.05, 0, 0), Space.World);
        }



    }
}
