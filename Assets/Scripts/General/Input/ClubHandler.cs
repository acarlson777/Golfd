using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubHandler : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float convergeConstant;

    [SerializeField] private GameObject clubBody;
    [SerializeField] private GameObject clubHead;
    [SerializeField] private float maxClubLength;
    private Ray toGroundRay;
    private RaycastHit groundHit;
    [SerializeField] private float clubLength;
    [SerializeField] private LayerMask layerToHit;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateClubPosition();
        UpdateClubLength(clubBody.transform, clubBody.transform.forward); //Use layermasks to avoid the raycast hitting the clubhead
        UpdateClubHeadPosition();
    }

    private void UpdateClubPosition()
    {
        transform.position += (camera.transform.position - transform.position) / convergeConstant;
        transform.rotation = camera.transform.rotation;
    }

    private void UpdateClubLength(Transform activeTransform, Vector3 activeDirection)
    {
        toGroundRay = new Ray(activeTransform.position, activeDirection.normalized);
        if (Physics.Raycast(toGroundRay, out groundHit, 100, layerToHit))
        {
            clubLength = Mathf.Min(groundHit.distance, maxClubLength);
            print("Ray hit " + groundHit.collider.gameObject.name);
            Debug.DrawLine(activeTransform.position, activeDirection.normalized * 10, Color.green, 1f);
        } else
        {
            print("Ray hit nothing");
            Debug.DrawLine(activeTransform.position, activeDirection.normalized * 10, Color.red, 1f);
        }

        
    }

    private void UpdateClubHeadPosition()
    {
        clubHead.transform.localPosition = new Vector3(clubHead.transform.localPosition.x, clubHead.transform.localPosition.y, clubLength);
    }
}
