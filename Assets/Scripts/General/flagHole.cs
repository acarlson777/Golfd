using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagHole : MonoBehaviour
{
    public HoleHandler golfBallInHole;
    public Animator flagEnd;
    public Animation FlagRaise;
        

        

    // Start is called before the first frame update
    void Start()
    {
        flagEnd = gameObject.GetComponent<Animator>();
        FlagRaise = gameObject.GetComponent<Animation>();
        flagEnd.ResetTrigger("FlagRaise");
    }

    // Update is called once per frame
    void Update()
    {
        if (golfBallInHole == true)
        {

            flagEnd.SetTrigger("FlagRaise");
        }

        if (golfBallInHole == false)
        {

            flagEnd.ResetTrigger("FlagRaise");
        }
    }
    
}
