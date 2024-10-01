using UnityEngine;
using System.Collections;

public class TransformRigidBody : MonoBehaviour
{
    [SerializeField] private float velocityConstant;
    [SerializeField] private Vector3 lastPosition;
    [SerializeField] private Vector3 currPosition;
    [SerializeField] private Vector3 deltaPosition;

    [SerializeField] private float angularVelocityConstant;
    [SerializeField] private Quaternion lastRotation;
    [SerializeField] private Quaternion currRotation;
    [SerializeField] private Vector3 deltaRotation;

    private void Start()
    {
        lastPosition = currPosition = transform.position;
        lastRotation = currRotation = transform.rotation;
    }

    private void Update()
    {
        currPosition = transform.position;
        deltaPosition = (currPosition - lastPosition) * Time.deltaTime;
        lastPosition = currPosition;

        currRotation = transform.rotation;
        deltaRotation = (currRotation.eulerAngles - lastRotation.eulerAngles) * Time.deltaTime;
        lastRotation = currRotation;
    }

    public Vector3 GetRawTransformVelocity()
    {
        return deltaPosition;
    }

    public Vector3 GetRawTransformAngularVelocity()
    {
        return deltaRotation;
    }

    public Vector3 GetScaledTransformVelocity()
    {
        return deltaPosition * velocityConstant;
    }

    public Vector3 GetScaledTransformAngularVelocity()
    {
        return deltaRotation * angularVelocityConstant;
    }
}
