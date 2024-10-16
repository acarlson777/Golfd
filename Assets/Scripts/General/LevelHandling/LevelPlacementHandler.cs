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
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aRRayCastManager = GetComponent<ARRaycastManager>();
        pressPosition = playerInput.actions.FindAction("PressPosition");
    }

    public void OnScreenPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (aRRayCastManager.Raycast(pressPosition.ReadValue<Vector2>(), hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;

                if (!hasTapOccured)
                {
                    hasTapOccured = true;
                    WorldHandler.Instance.LoadNextLevel();
                    WorldHandler.Instance.UpdateLevelPosition(hitPose);
                    golfClub.SetActive(true);
                }
            }
        }
    }
}