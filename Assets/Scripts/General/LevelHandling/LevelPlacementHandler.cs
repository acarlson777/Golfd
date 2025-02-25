using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

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
                    }
                    else if (isMovingCurrentLevel)
                    {
                        isMovingCurrentLevel = false;
                        aRPlaneManager.requestedDetectionMode = PlaneDetectionMode.None;
                        WorldHandler.Instance.UpdateLevelPosition(hitPose);
                    }
                }
            }
        }
    }

    public void MoveCurrLevel()
    {
        isMovingCurrentLevel = true;
        aRPlaneManager.requestedDetectionMode = PlaneDetectionMode.Horizontal;
    }
}