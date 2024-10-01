using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClubHandler : MonoBehaviour
{
    [SerializeField] private new Camera camera;

    [SerializeField] private GameObject clubBody;
    [SerializeField] private GameObject clubHead;
    [SerializeField] private Vector3 clubHeadOffset;
    private Rigidbody rb;
    [SerializeField] private float maxClubLength;
    private Ray toGroundRay;
    private RaycastHit groundHit;
    private float clubLength;
    [SerializeField] private LayerMask layerToHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        UpdateClubPosition();
        UpdateClubLength(clubBody.transform, clubBody.transform.forward);
        UpdateClubHeadPosition();
    }

    private void UpdateClubPosition()
    {
        rb.MovePosition(camera.transform.position);
        rb.MoveRotation(camera.transform.rotation);
    }

    private void UpdateClubLength(Transform activeTransform, Vector3 activeDirection)
    {
        toGroundRay = new Ray(activeTransform.position, activeDirection.normalized);
        if (Physics.Raycast(toGroundRay, out groundHit, 100, layerToHit))
        {
            clubLength = Mathf.Min(groundHit.distance, maxClubLength);
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
        clubHead.transform.position = clubBody.transform.TransformPoint(new Vector3(clubBody.transform.localPosition.x + clubHeadOffset.x, clubBody.transform.localPosition.y + clubHeadOffset.y, clubLength - clubHead.transform.localScale.z / 3 + clubHeadOffset.z));
    }


    public void OnScreenPress(InputAction.CallbackContext context)
    {
        print("Screen Pressed 2");
    }
}