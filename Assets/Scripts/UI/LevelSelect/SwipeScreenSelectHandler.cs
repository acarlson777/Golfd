using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public abstract class SwipeScreenSelectHandler : MonoBehaviour, IEndDragHandler
{
    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private RectTransform _levelPagesRect;
    [SerializeField] private float _tweenTime;
    [SerializeField] private LeanTweenType tweenType;
    protected int maxPage;
    private int currentPage = 1;

    protected abstract int SetMaxPage();

    private void Awake()
    {
        maxPage = SetMaxPage();
    }

    private void Update()
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}
