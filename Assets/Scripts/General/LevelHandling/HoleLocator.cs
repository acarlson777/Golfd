using UnityEngine;
using System.Collections;

public class HoleLocator : MonoBehaviour
{
    [SerializeField] private Vector3 distanceFromClubToHole;
    [SerializeField] private float angleToHole;
    [SerializeField] private Vector3 cameraAngle;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Vector3 arrowPositionOffset;

    void Update()
    {
        if (WorldHandler.Instance.currLevelHandler == null) { return; }
        distanceFromClubToHole = WorldHandler.Instance.currLevelHandler.hole.transform.position - (Camera.main.gameObject.transform.position+ arrowPositionOffset);
        if (distanceFromClubToHole.x < 0)
        {
            angleToHole = Mathf.Atan(distanceFromClubToHole.z / distanceFromClubToHole.x) * Mathf.Rad2Deg + 180;
        } else
        {
            angleToHole = Mathf.Atan(distanceFromClubToHole.z / distanceFromClubToHole.x) * Mathf.Rad2Deg; //works for +x +z and +x -z (some constant angles needs to be added in other situations (probably 90 or 180)
        }

        cameraAngle = new Vector3(Camera.main.transform.rotation.eulerAngles.x, (Camera.main.transform.rotation.eulerAngles.y + angleToHole)%360, Camera.main.transform.rotation.eulerAngles.z);

        arrow.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, cameraAngle.y+180);
    }
}
