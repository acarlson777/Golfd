using UnityEngine;
using System.Collections;

public class HoleHandler : MonoBehaviour
{
    [SerializeField] private LevelHandler _levelHandler;
    private float _ballVelocityTolerance = 1f;
    private bool golfBallInHole;
    private Coroutine currCoroutine;

    private void OnTriggerEnter(Collider other)
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

    private void OnTriggerExit(Collider other)
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
