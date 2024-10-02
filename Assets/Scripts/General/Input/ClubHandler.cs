using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClubHandler : MonoBehaviour
{
    [SerializeField] private new Camera _camera;

    [SerializeField] private GameObject _clubBody;
    [SerializeField] private GameObject _clubHead;
    [SerializeField] private Vector3 _clubHeadOffset;
    private Rigidbody rb;
    [SerializeField] private float _maxClubLength;
    private Ray toGroundRay;
    private RaycastHit groundHit;
    private float clubLength;
    [SerializeField] private LayerMask _layerToHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateClubPosition();
        UpdateClubLength(_clubBody.transform, _clubBody.transform.forward);
        UpdateClubHeadPosition();
    }

    private void UpdateClubPosition()
    {
        rb.MovePosition(_camera.transform.position);
        rb.MoveRotation(_camera.transform.rotation);
    }

    private void UpdateClubLength(Transform activeTransform, Vector3 activeDirection)
    {
        toGroundRay = new Ray(activeTransform.position, activeDirection.normalized);
        if (Physics.Raycast(toGroundRay, out groundHit, 100, _layerToHit))
        {
            clubLength = Mathf.Min(groundHit.distance, _maxClubLength);
            //print("Ray hit " + groundHit.collider.gameObject.name);
            Debug.DrawLine(activeTransform.position, activeDirection.normalized * 10, Color.green, 1f);
        }
        else
        {
            //print("Ray hit nothing");
            Debug.DrawLine(activeTransform.position, activeDirection.normalized * 10, Color.red, 1f);
        }
    }

    private void UpdateClubHeadPosition()
    {
        _clubHead.transform.position = _clubBody.transform.TransformPoint(new Vector3(_clubBody.transform.localPosition.x + _clubHeadOffset.x, _clubBody.transform.localPosition.y + _clubHeadOffset.y, clubLength - _clubHead.transform.localScale.z / 3 + _clubHeadOffset.z));
    }


    public void OnScreenPress(InputAction.CallbackContext context)
    {
        print("Screen Pressed 2");
    }
}