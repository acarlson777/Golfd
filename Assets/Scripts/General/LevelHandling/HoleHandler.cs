using UnityEngine;
using System.Collections;

public class HoleHandler : MonoBehaviour
{
    [SerializeField] private LevelHandler _levelHandler;
    private float golfBallBounceOutCheckDuration = 1f;
    private bool golfBallInHole = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            golfBallInHole = true;
            StartCoroutine(CheckForBounceOut());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            golfBallInHole = false;
        }
    }

    private IEnumerator CheckForBounceOut()
    {
        yield return new WaitForSeconds(golfBallBounceOutCheckDuration);
        if (golfBallInHole)
        {
            WorldHandler.Instance.OnLevelCompleted();
        }
    }
}
