using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class LevelPlacementHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction pressPosition;
    [SerializeField] private GameObject golfClub;
    private bool hasTapOccured = false;

    private ARRaycastManager aRRayCastManager;
    private ARPlaneManager aRPlaneManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isMovingCurrentLevel = false;
    public bool canPlaceLevel = true;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject movingLevelWarning;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aRRayCastManager = GetComponent<ARRaycastManager>();
        aRPlaneManager = GetComponent<ARPlaneManager>();
        pressPosition = playerInput.actions.FindAction("PressPosition");
    }

    public void OnScreenPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (aRRayCastManager.Raycast(pressPosition.ReadValue<Vector2>(), hits, TrackableType.PlaneWithinPolygon))
            {
                Pose hitPose = hits[0].pose;
                if (canPlaceLevel)
                {
                    if (!hasTapOccured)
                    {
                        hasTapOccured = true;
                        aRPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
                        WorldHandler.Instance.UpdateLevelPosition(hitPose);
                        WorldHandler.Instance.LoadNextLevel();
                        golfClub.SetActive(true);
                        ChangePlaneVisibility(false);
                    }
                    else if (isMovingCurrentLevel)
                    {
                        movingLevelWarning.SetActive(false);
                        isMovingCurrentLevel = false;
                        aRPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
                        WorldHandler.Instance.UpdateLevelPosition(hitPose);
                        ChangePlaneVisibility(false);
                    }
                }
            }
        }
    }

    public void MoveCurrLevel()
    {
        isMovingCurrentLevel = true;
        aRPlaneManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;

        ChangePlaneVisibility(true);

        movingLevelWarning.SetActive(true);
        settingsButton.onClick.Invoke();
    }

    private void ChangePlaneVisibility(bool visibility)
    {
        Transform trackablesTransform = gameObject.transform.Find("Trackables");
        for (int i = 0; i < trackablesTransform.childCount; i++)
        {
            trackablesTransform.GetChild(i).gameObject.SetActive(visibility);
        }
    }
}