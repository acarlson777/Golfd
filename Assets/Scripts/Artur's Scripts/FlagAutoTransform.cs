using UnityEngine;

public class GolfBallTriggerController : MonoBehaviour
{  
    public float moveHeight = 2f;
    public float animationTime = 0.5f;

    public Vector3 originalPosition;
    public Vector3 targetPosition;
    private bool isRaised = false;

    void Start()
    {
        originalPosition = gameObject.transform.localPosition;
        targetPosition = originalPosition + new Vector3(0, moveHeight, 0);
    }

    void OnTriggerEnter(Collider other){

        Debug.Log("enter");

        if (other.CompareTag("GolfBall"))
        {
            LeanTween.moveLocal(gameObject, targetPosition, animationTime).setEaseOutQuad();
            isRaised = true;
        }
    }

    void OnTriggerExit(Collider other){

        Debug.Log("exit");

        if (other.CompareTag("GolfBall"))
        {
            LeanTween.moveLocal(gameObject, originalPosition, animationTime).setEaseInQuad();
            isRaised = false;
        }
    }
}
