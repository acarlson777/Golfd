using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClubHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _clubBody;
    public GameObject _clubHead;
    [SerializeField] private Vector3 _clubHeadOffset;
    private Rigidbody rb;
    private Rigidbody gBrb;
    [SerializeField] private float _minClubLength;
    [SerializeField] private float _maxClubLength;
    private Ray toGroundRay;
    private RaycastHit groundHit;
    private float clubLength;
    [SerializeField] private LayerMask _layerToHit;
    private GameObject golfBall = null;
    [SerializeField] private float _ballVelocityTolerance;
    [SerializeField] private float _swingTime;
    [SerializeField] private GameObject throwableClubPrefab;
    private Vector3 clubVelocity;
    private Vector3 lastClubPos;
    private Vector3 clubRotationalVelocity;
    private Quaternion lastClubRot;
    [SerializeField] private float ROTATIONAL_DAMPENING;
    [SerializeField] private float CLUB_LOFT;
    [SerializeField] private float EXTRA_FORWARD_BOOST;
    private bool canThrowClubs = false;
    [SerializeField] private Animator clubThrowAnimator;
    public bool clubEnabled = true;
    private bool canToggleClubThrowing = true;
    private bool attemptedToUpdateCanToggleClubThrowing = false;

    public GolfBallIndicatorHandler ballIndicator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        canThrowClubs = PlayerPrefs.GetInt("canThrowClubs") == 1;
        clubThrowAnimator.SetBool("isOn", canThrowClubs);
    }

    private void Update()
    {
        if (!clubEnabled)
        {
            return;
        }

        UpdateClubPosition();
        UpdateClubLength(_clubBody.transform, _clubBody.transform.forward);
        UpdateClubHeadPosition();
        UpdateClubHeadColliderStatus();
        UpdateClubVelocity();
        FindGolfBall();
    }

    private void FindGolfBall()
    {
        if (GameObject.FindGameObjectWithTag("GolfBall") != null) {
            golfBall = GameObject.FindGameObjectWithTag("GolfBall");
            gBrb = golfBall.GetComponent<Rigidbody>();



        }
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
            _minClubLength = clubLength;
            //print("Ray hit " + groundHit.collider.gameObject.name);
            Debug.DrawLine(activeTransform.position, activeDirection.normalized * 10, Color.green, 1f);
        }
        else
        {
            clubLength = _minClubLength;
            //print("Ray hit nothing");
            Debug.DrawLine(activeTransform.position, activeDirection.normalized * 10, Color.red, 1f);
        }
    }

    private void UpdateClubHeadPosition()
    {
        _clubHead.transform.position = _clubBody.transform.TransformPoint(new Vector3(_clubBody.transform.localPosition.x + _clubHeadOffset.x, _clubBody.transform.localPosition.y + _clubHeadOffset.y, clubLength - _clubHead.transform.localScale.z / 3 + _clubHeadOffset.z));
    }

    private void UpdateClubHeadColliderStatus()
    {
        if (gBrb == null) { return; }
        if (gBrb.velocity.magnitude <= _ballVelocityTolerance)
        {

            ballIndicator.gameObject.SetActive(true);
            //Ball slowed down enough to count as stoppped 
            WorldHandler.Instance.UpdateLastKnownBallPos();
            _clubHead.GetComponent<BoxCollider>().enabled = true;
            //clubEnabled = true;
        }
    }

    private void UpdateClubVelocity()
    {
        clubVelocity = (rb.transform.position - lastClubPos) / Time.deltaTime;
        lastClubPos = rb.transform.position;

        var deltaRot = transform.rotation * Quaternion.Inverse(lastClubRot);
        var eulerRot = new Vector3(Mathf.DeltaAngle(0, deltaRot.eulerAngles.x), Mathf.DeltaAngle(0, deltaRot.eulerAngles.y), Mathf.DeltaAngle(0, deltaRot.eulerAngles.z));

        clubRotationalVelocity = eulerRot / Time.fixedDeltaTime;
        lastClubRot = rb.transform.rotation;
    }

    public void OnScreenPressOrRelease(InputAction.CallbackContext context)
    {
        if (!clubEnabled)
        {
            return;
        }

        if (context.started) {
            //print("press or released");
            _clubHead.SetActive(true);
        } else if (context.canceled)
        {
            if (_clubHead.activeInHierarchy)
            {
                if (canThrowClubs)
                {
                    GameObject thrownClub = Instantiate(throwableClubPrefab, rb.transform.position, rb.transform.rotation);
                    thrownClub.GetComponent<Rigidbody>().velocity = clubVelocity + rb.transform.TransformPoint(new Vector3(0, CLUB_LOFT, EXTRA_FORWARD_BOOST));
                    thrownClub.GetComponent<Rigidbody>().angularVelocity = clubRotationalVelocity * ROTATIONAL_DAMPENING;
                }
                _clubHead.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GolfBall"))// && _clubHead.GetComponent<BoxCollider>().enabled)
        {
            StartCoroutine(SwingTime());
        }
    }

    IEnumerator SwingTime()
    {
        yield return new WaitForSeconds(_swingTime);
        _clubHead.GetComponent<BoxCollider>().enabled = false;
        if (gBrb.velocity.magnitude > _ballVelocityTolerance)
        {

            ballIndicator.gameObject.SetActive(false);
            //Ball was hit fast enough to count as a real hit
            WorldHandler.Instance.IncrementStrokeCount();



            //clubEnabled = false;
            //_clubHead.SetActive(false);
        }
    }

    public void ToggleClubThrowing()
    {
        if (!gameObject.activeInHierarchy) { return; }
        if (!canToggleClubThrowing && !attemptedToUpdateCanToggleClubThrowing) { return; }
        canThrowClubs = !canThrowClubs;
        PlayerPrefs.SetInt("canThrowClubs", canThrowClubs ? 1 : 0);
        canToggleClubThrowing = false;
        clubThrowAnimator.SetTrigger("isAnimating");
        clubThrowAnimator.SetBool("isOn", canThrowClubs);
        StartCoroutine(ReactivateTogglingOfClubThrowing());
    }

    private IEnumerator ReactivateTogglingOfClubThrowing()
    {
        attemptedToUpdateCanToggleClubThrowing = true;
        yield return new WaitForSeconds(0.33f);
        canToggleClubThrowing = true;
        attemptedToUpdateCanToggleClubThrowing = false;
    }   
}