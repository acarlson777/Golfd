using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour
{
    private float golfBallInHoleToLevelCompleteTransitionTime = 1f;
    public int par;

    private void Start()
    {
        
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
}
