using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class SwipeScreenSelectHandler : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private Vector3 _pageStep;
    [SerializeField] private RectTransform _pagesRect;
    [SerializeField] private float _tweenTime;
    [SerializeField] private LeanTweenType _tweenType;
    [SerializeField] private int _maxPage;
    [SerializeField] bool _isWorldSwipeScreenHandler;
    private LevelPrefabCamera levelPrefabCamera;
    private Vector3 targetPos;
    private int currentPage = 1;
    private float dragThreshold;

    [SerializeField] private GameObject golfCart;
    [SerializeField] private float lengthOfRotation;
    [SerializeField] private LeanTweenType easeType;
    private LTDescr currentLeanTween = null;

    //Follow this tutorial to create the level select screen: https://www.youtube.com/watch?v=qf9xe2mbWeU

    private void Awake()
    {
        targetPos = _pagesRect.localPosition;
        dragThreshold = Screen.width / 15;
        levelPrefabCamera = GameObject.FindGameObjectWithTag("LevelPrefabCamera").GetComponent<LevelPrefabCamera>();
    }

    public void Next()
    {
        if (currentPage < _maxPage)
        {
            currentPage++;
            targetPos += _pageStep;
            MovePage();
            golfCartRotation(1);
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= _pageStep;
            MovePage();
            golfCartRotation(-1);
        }
    }

    private void MovePage()
    {
        _pagesRect.LeanMoveLocal(targetPos, _tweenTime).setEase(_tweenType);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_isWorldSwipeScreenHandler)
        {
            levelPrefabCamera.GenerateCrossWorldLevels(_pageStep);
        } else
        {
            levelPrefabCamera.GenerateSameWorldLevels(_pageStep);
        }
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

    private void golfCartRotation(int endScale)
    {
        if (currentLeanTween != null)
        {
            //currentLeanTween.cancel(golfCart);
        }
        currentLeanTween = LeanTween.scaleX(golfCart, endScale, lengthOfRotation)
         .setEase(easeType);
    }
}
