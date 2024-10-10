using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour
{
    public int par;
    [SerializeField] private Vector3 _animateInPosition;
    [SerializeField] private Vector3 _animateOutPosition;
    [SerializeField] private float _animateInDuration;
    [SerializeField] private float _animateOutDuration;

    private void Start()
    {
        transform.position = _animateOutPosition;
    }

    public void AnimateIn()
    {
        StartCoroutine(AnimateInCoroutine());
    }

    public void AnimateOut()
    {
        StartCoroutine(AnimateOutCoroutine());
    }

    private IEnumerator AnimateInCoroutine()
    {
        float timeElapsed = 0;
        while (timeElapsed <= _animateInDuration)
        {
            transform.position = Vector3.Lerp(_animateInPosition, _animateOutPosition, timeElapsed / _animateInDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator AnimateOutCoroutine()
    {
        float timeElapsed = 0;
        while (timeElapsed <= _animateOutDuration)
        {
            transform.position = Vector3.Lerp(_animateOutPosition, _animateInPosition, timeElapsed / _animateOutDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
