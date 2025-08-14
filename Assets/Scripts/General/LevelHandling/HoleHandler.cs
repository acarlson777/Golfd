using UnityEngine;
using System.Collections;

public class HoleHandler : MonoBehaviour
{
    [SerializeField] private int penalty = 0;
    [SerializeField] private LevelHandler _levelHandler;
    private float _ballVelocityTolerance = 0.1f;
    public bool golfBallInHole; // made the classes public for flagHole script. Alex a
    public Coroutine currCoroutine;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private AudioSource golfBallInHoleSound;
    [SerializeField] private AudioClip[] possibleGolfClaps;

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

    public IEnumerator CheckForGolfBallStopped(Collider other)
    {
        Rigidbody gBrb = other.gameObject.GetComponent<Rigidbody>();

        while (gBrb.velocity.magnitude > _ballVelocityTolerance)
        {
            //print("Checking for golf ball stopped");
            yield return null;
        }
        AudioClip selectedGolfClapClip = possibleGolfClaps[Random.Range(0, possibleGolfClaps.Length - 1)];
        particleSystem.Play();
        golfBallInHoleSound.Play();
        AudioSource.PlayClipAtPoint(selectedGolfClapClip, transform.position);

        WorldHandler.Instance.OnLevelCompleted(penalty);
    }
}
