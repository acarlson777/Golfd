using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagHole : MonoBehaviour
{
    public HoleHandler golfBallInHole;
    public GameObject FlagHolder;
    public Animator FlagController;
    public Animation FlagRaise;
    




    // Start is called before the first frame update
    void Start()
    {
        
        FlagController = gameObject.GetComponent<Animator>();
        FlagRaise = gameObject.GetComponent<Animation>();
        FlagController.ResetTrigger("FlagRaise");
        FlagHolder.SetActive(false);
        Debug.Log("flag ready");
    }

    // Update is called once per frame
    void Update()
    {
        if (golfBallInHole)
            
        {

            FlagController.SetTrigger("FlagRaise");
            FlagHolder.SetActive(true);
            Debug.Log("flag");
            FlagRaise.Play("FlagRaise");
        }

        if (!golfBallInHole)
        {

            FlagController.ResetTrigger("FlagRaise");
            FlagHolder.SetActive(false);
            Debug.Log("flag gone");
            FlagRaise.Stop("FlagRaise");
        }
    }

}
