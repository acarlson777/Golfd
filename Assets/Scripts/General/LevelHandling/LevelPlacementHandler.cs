using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class LevelPlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject golfClub;

    private GameObject levelToPlace;
    private GameObject spawnedObject;
    private ARRaycastManager aRRayCastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private PlayerInput playerInput;
    private InputAction pressPosition;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        pressPosition = playerInput.actions.FindAction("PressPosition");
    }

    private void Start()
    {
        //Show text which tells the player to look around and that anywhere with dots the level can be placed
        levelToPlace = WorldHandler.Instance.GetCurrentLevelGameObject();
    }

    public void OnScreenPress(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (aRRayCastManager.Raycast(pressPosition.ReadValue<Vector2>(), hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPose = hits[0].pose;

                if (spawnedObject == null)
                {
                    spawnedObject = Instantiate(levelToPlace, hitPose.position, hitPose.rotation);
                    spawnedObject.GetComponent<LevelHandler>().AnimateIn();
                    golfClub.SetActive(true);
                    this.enabled = false;
                }
            }
        }
    }
}
