using UnityEngine;
using System.Collections;

public class HoleHandler : MonoBehaviour
{
    [SerializeField] private LevelHandler _levelHandler;
    private float _ballVelocityTolerance = 0.1f;
    public bool golfBallInHole; //Alex: changed var and classes from private to public for flagHole script
    private Coroutine currCoroutine;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            if (!golfBallInHole)
            {
                currCoroutine = StartCoroutine(CheckForGolfBallStopped(other));
                golfBallInHole = true;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            StopCoroutine(currCoroutine);
            currCoroutine = null;
            golfBallInHole = false;
        }
    }

    private IEnumerator CheckForGolfBallStopped(Collider other)
    {
        Rigidbody gBrb = other.gameObject.GetComponent<Rigidbody>();

        while (gBrb.velocity.magnitude > _ballVelocityTolerance)
        {
            //print("Checking for golf ball stopped");
            yield return null;
        }
        WorldHandler.Instance.OnLevelCompleted();
    }
}
