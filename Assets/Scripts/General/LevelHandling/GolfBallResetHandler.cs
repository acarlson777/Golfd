using UnityEngine;
using System.Collections;

public class GolfBallResetHandler : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            WorldHandler.Instance.ResetBallPosToLastKnownPos();
        }
    }
}
