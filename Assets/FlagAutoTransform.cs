using UnityEngine;

public class GolfBallTriggerController : MonoBehaviour
{
    public GameObject targetObject;   
    public float moveHeight = 2f;
    public float animationTime = 0.5f;

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isRaised = false;

    void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target Object not assigned!");
            return;
        }

        originalPosition = targetObject.transform.position;
        targetPosition = originalPosition + new Vector3(0, moveHeight, 0);
    }

    void OnTriggerEnter(Collider other){

         Debug.Log("enter");

        if (other.CompareTag("GolfBall"))
        {
            LeanTween.move(targetObject, targetPosition, animationTime).setEaseOutQuad();
            isRaised = true;
        }
    }

    void OnTriggerExit(Collider other){

        Debug.Log("exit");

        if (other.CompareTag("GolfBall"))
        {
            LeanTween.move(targetObject, originalPosition, animationTime).setEaseInQuad();
            isRaised = false;
        }
    }
}
