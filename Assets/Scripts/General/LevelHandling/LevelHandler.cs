using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour
{
    public int par;
    [SerializeField] private Vector3 _animateStartPosition;
    private Vector3 _animateEndPosition;
    private float _animateInDuration = 5f;
    private float _animateOutDuration = 5f;
    public GameObject LEVEL;
    private float timeElapsed;

    private void Start()
    {
        LEVEL.transform.position = _animateStartPosition;
        _animateEndPosition = new Vector3(0, 0, 0);
    }

    public IEnumerator AnimateInCoroutine()
    {
        timeElapsed = 0;
        while (timeElapsed <= _animateInDuration)
        {
            LEVEL.transform.position = Vector3.Lerp(_animateStartPosition, _animateEndPosition, timeElapsed / _animateInDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator AnimateOutCoroutine()
    {
        timeElapsed = 0;
        while (timeElapsed <= _animateOutDuration)
        {
            LEVEL.transform.position = Vector3.Lerp(_animateEndPosition, _animateStartPosition, timeElapsed / _animateOutDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
