using UnityEngine;
using System.Collections;

public class HoleLocator : MonoBehaviour
{
    [SerializeField] private Vector3 distanceFromClubToHole;
    [SerializeField] private float angleToHole;
    [SerializeField] private Vector3 cameraAngle;

    void Update()
    {
        if (WorldHandler.Instance.currLevelHandler == null) { return; }
        distanceFromClubToHole = WorldHandler.Instance.currLevelHandler.hole.transform.position - Camera.main.gameObject.transform.position;
        angleToHole = Mathf.Atan(distanceFromClubToHole.z / distanceFromClubToHole.x)*Mathf.Rad2Deg; //works for +x +z and +x -z (some constant angles needs to be added in other situations (probably 90 or 180)
        cameraAngle = Camera.main.transform.rotation.eulerAngles;
    }
}
