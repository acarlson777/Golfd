using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour
{
    private float golfBallInHoleToLevelCompleteTransitionTime = 1f;
    public int par;
    [SerializeField] private Vector3 animateInPosition;
    private Vector3 animateOutPosition;

    //https://www.youtube.com/watch?v=7vFwTt4isDY <-- Use this tutorial for masking

    private void Start()
    {
        animateOutPosition = transform.position;
    }

    public void AnimateIn()
    {
        
    }

    public void AnimateOut()
    {

    }

    public IEnumerator OnLevelCompleted()
    {
        yield return new WaitForSeconds(golfBallInHoleToLevelCompleteTransitionTime);
        WorldHandler.Instance.OnLevelCompleted();
    }

    private IEnumerator AnimateInCoroutine()
    {
        yield return null;
    }

    private IEnumerator AnimateOutCoroutine()
    {
        yield return null;
    }
}
