using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SwipeScreenSelectHandler : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private RectTransform _levelPagesRect;
    [SerializeField] private float _tweenTime;
    [SerializeField] private LeanTweenType tweenType;
    [SerializeField] private int _maxPage;
    private Vector3 targetPos;
    private int currentPage = 1;
    private float dragThreshold;

    //Follow this tutorial to create the level select screen: https://www.youtube.com/watch?v=qf9xe2mbWeU

    private void Awake()
    {
        targetPos = _levelPagesRect.localPosition;
        dragThreshold = Screen.width / 15;
    }

    public void Next()
    {
        if (currentPage < _maxPage)
        {
            currentPage++;
            targetPos += _pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= _pageStep;
            MovePage();
        }
    }

    private void MovePage()
    {
        _levelPagesRect.LeanMoveLocal(targetPos, _tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x)
            {
                Previous();
            }
            else
            {
                Next();
            }
        } else
        {
            MovePage();
        }
    }

    public void SetMaxPage(int maxPage)
    {
        this._maxPage = maxPage;
    }
}
