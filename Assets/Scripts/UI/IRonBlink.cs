using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IRonBlink : MonoBehaviour
{
    //Minimum & maximum seconds that can happen bfore IRon can blink
    public float minSec = 2f;
    public float maxSec = 7f;
    // Start is called before the first frame update
    private void Start()
    {   

    }

    // Update is called once per frame
    private IEnumerator Entry()
    {
        while (true){
            //determines the interval between iRon blinking again
            float waitTime = Random.Range(minSec, maxSec);
            yield return new
            WaitForSeconds(waitTime);
            //Plays animation of IRon blinking after the wait but it no work :(
            this.GetComponent<Animator>().Play("BlinkingIRon");
        }
    }
}
