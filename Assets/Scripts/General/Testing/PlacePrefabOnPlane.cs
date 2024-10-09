using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class PlacePrefabOnPlane : MonoBehaviour
{
    [SerializeField] private string _sceneToGoTo;

    private PlayerInput playerInput;
    private InputAction press;
    private InputAction pressPosition;

    [SerializeField] private GameObject _placedPrefab;
    private GameObject spawnedObject;
    private ARRaycastManager aRRayCastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aRRayCastManager = GetComponent<ARRaycastManager>();
        press = playerInput.actions.FindAction("Press");
        pressPosition = playerInput.actions.FindAction("PressPosition");
    }

    private void OnEnable()
    {
        //press.performed += OnScreenPress;
    }

    private void OnDisable()
    {
        //press.performed -= OnScreenPress;
    }

    private void OnScreenPress(InputAction.CallbackContext context)
    {
        //SceneHandler.Instance.LoadScene(_sceneToGoTo);

        print("Screen Pressed 1");

        if (aRRayCastManager.Raycast(pressPosition.ReadValue<Vector2>(), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if (spawnedObject == null)
            {
                spawnedObject = Instantiate(_placedPrefab, hitPose.position, hitPose.rotation);
            } else
            {
                spawnedObject.transform.position = hitPose.position;
                spawnedObject.transform.rotation = hitPose.rotation;
            }
        }
    }
}