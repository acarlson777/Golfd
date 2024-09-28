using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClubHandler : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float convergeConstant;

    [SerializeField] private GameObject clubBody;
    [SerializeField] private GameObject clubHead;
    [SerializeField] private float maxClubLength;
    [SerializeField] private GameObject ENVIRONMENT;
    [SerializeField] private float THROW_FORCE;
    private Ray toGroundRay;
    private RaycastHit groundHit;
    private float clubLength;
    [SerializeField] private LayerMask layerToHit;
    private bool isClubActive = false;

    private void Start()
    {

    }

    private void Update()
    {
        UpdateClubPosition();
        UpdateClubLength(clubBody.transform, clubBody.transform.forward);
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
        clubHead.transform.localPosition = new Vector3(clubHead.transform.localPosition.x, clubHead.transform.localPosition.y, clubLength - clubHead.transform.localScale.z / 3);
    }

    
    public void OnScreenPress(InputAction.CallbackContext context)
    {
        print("Screen Pressed");

        /*
        print("toggled club");

        if (isClubActive)
        {
            clubBody.transform.parent = ENVIRONMENT.transform;
            clubBody.GetComponent<Rigidbody>().isKinematic = false;
            clubBody.GetComponent<Rigidbody>().AddForce((transform.forward.normalized)*THROW_FORCE, ForceMode.Impulse);
            clubBody.GetComponent<Rigidbody>().useGravity = true;
            isClubActive = false;
        } else
        {
            isClubActive = true;
            clubBody.SetActive(true);
        }
        */
    }

}
