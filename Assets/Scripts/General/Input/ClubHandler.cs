using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubHandler : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float convergeConstant;

    [SerializeField] private GameObject clubBody;
    [SerializeField] private float maxClubLength;
    private Ray toGroundRay;
    private RaycastHit groundHit;

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateClubPosition();

        //Raycast is not being based off the correct local direction from clubBody
        UpdateClubLength(clubBody.transform, clubBody.transform.forward);
    }

    private void UpdateClubPosition()
    {
        transform.position += (camera.transform.position - transform.position) / convergeConstant;
        transform.rotation = camera.transform.rotation;
    }

    private void UpdateClubLength(Transform activeTransform, Vector3 activeDirection)
    {
        toGroundRay = new Ray(activeTransform.position, clubBody.transform.up);
        if (Physics.Raycast(toGroundRay, out groundHit, 100))
        {
            print("Ray hit " + groundHit.collider.gameObject.name);
            Debug.DrawLine(activeTransform.position, transform.TransformDirection(activeDirection) * groundHit.distance, Color.green, 1f);
        } else
        {
            print("Ray hit nothing");
            Debug.DrawLine(activeTransform.position, transform.TransformDirection(activeDirection) * 1000, Color.red, 1f);
        }
    }
}
