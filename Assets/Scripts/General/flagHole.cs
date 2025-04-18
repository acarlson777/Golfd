using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagHole : MonoBehaviour
{
    public HoleHandler holeHandler;
    public GameObject FlagHolder;
    public Animator FlagController;
    public Animation FlagRaise;



    private IEnumerator Golfcoroutine;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Check starting");
        FlagController = gameObject.GetComponent<Animator>();
        FlagRaise = gameObject.GetComponent<Animation>();
        
        Debug.Log("flag ready");
        Golfcoroutine = WaitAndCheck(2.0f);
        StartCoroutine(Golfcoroutine);
        Debug.Log("Check ready");
    }


    private IEnumerator WaitAndCheck(float waitTime)
    {
        Debug.Log("Check Commencing");
        if (holeHandler.golfBallInHole == true)

        {


            
            Debug.Log("flag");
            FlagRaise.Play("FlagRaise");
            yield return new WaitForSeconds(waitTime);
            FlagRaise.Stop("FlagRaise");


        }

        if (holeHandler.golfBallInHole == false)
        {


            
            Debug.Log("flag gone");
            FlagRaise.Rewind("FlagRaise");
        }
        Debug.Log("Check Finished");
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Check Commenced");
    }
        // Update is called once per frame
        void Update()
    {

        
    }

}
