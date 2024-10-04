using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour
{
    private int strokeCount = 0;
    private float golfBallInHoleToLevelCompleteTransitionTime = 1f;
    [SerializeField] private int _strokeCountNeededForPar;

    private void Start()
    {
        
    }

    public void AnimateIn()
    {

    }

    public void AnimateOut()
    {

    }

    private int GetStrokeCount()
    {
        return strokeCount;
    }

    public void IncrementStrokeCount()
    {
        strokeCount++;
    }

    public IEnumerator OnLevelCompleted()
    {
        yield return new WaitForSeconds(golfBallInHoleToLevelCompleteTransitionTime);
        WorldHandler.Instance.OnLevelCompleted();
    }
}
