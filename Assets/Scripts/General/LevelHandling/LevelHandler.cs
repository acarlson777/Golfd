using UnityEngine;
using System.Collections;

public class LevelHandler : MonoBehaviour
{
    public int par;
    [SerializeField] private float _animateStartHeight;
    [SerializeField] private float _animateEndHeight;
    [SerializeField] public float _outsideEditorHeightOffset;
    private float _animateInDuration = 1f;
    private float _animateOutDuration = 1f;
    public GameObject LEVEL;
    private float timeElapsed;

    private void Start()
    {
        SetLevelTransformY(_animateStartHeight);
        _animateEndHeight = 0;
#if UNITY_EDITOR
        return;
#endif
#pragma warning disable CS0162 // Unreachable code detected
        //_animateStartHeight += _outsideEditorHeightOffset;
#pragma warning restore CS0162 // Unreachable code detected
        //_animateEndHeight += _outsideEditorHeightOffset;
    }

    public IEnumerator AnimateInCoroutine()
    {
        timeElapsed = 0;
        while (timeElapsed <= _animateInDuration)
        {
            SetLevelTransformY(Mathf.Lerp(_animateStartHeight, _animateEndHeight, timeElapsed / _animateInDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        SetLevelTransformY(_animateEndHeight);
    }

    public IEnumerator AnimateOutCoroutine()
    {
        timeElapsed = 0;
        while (timeElapsed <= _animateOutDuration)
        {
            SetLevelTransformY(Mathf.Lerp(_animateEndHeight, _animateStartHeight, timeElapsed / _animateOutDuration));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        SetLevelTransformY(_animateStartHeight);
    }

    public void SetLevelTransformY(float newY)
    {
        LEVEL.transform.position = new Vector3(LEVEL.transform.position.x, newY, LEVEL.transform.position.z);
    }
}
