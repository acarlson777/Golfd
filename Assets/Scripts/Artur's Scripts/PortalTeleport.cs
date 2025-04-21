using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform portalA;
    public Transform portalB;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GolfBall"))
        {
            Debug.Log("touching!");

            // Access the ball's PortalTeleportState component
            PortalTeleportState ballState = other.GetComponent<PortalTeleportState>();
            if (ballState != null)
            {
                
                Debug.Log("found rigidbody");
                // Only teleport if the ball hasn't been teleported yet (flag check)
                if (transform == portalA && !ballState.hasTeleportedFromB)
                {
                    Debug.Log("PORTAL A -> PORTAL B");
                    other.transform.position = portalB.position;
                    ballState.hasTeleportedFromA = true;  // Set flag to prevent teleporting back to A
                }
                else if (transform == portalB && !ballState.hasTeleportedFromA)
                {
                    Debug.Log("PORTAL B -> PORTAL A");
                    other.transform.position = portalA.position;
                    ballState.hasTeleportedFromB = true;  // Set flag to prevent teleporting back to B
                }

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GolfBall"))
        {
            PortalTeleportState ballState = other.GetComponent<PortalTeleportState>();
            if (ballState != null)
            {
                // Reset teleport flags when the ball exits either portal
                if (transform == portalA)
                {
                    ballState.hasTeleportedFromB = false;  // Reset when exiting portal A
                }
                else if (transform == portalB)
                {
                    ballState.hasTeleportedFromA = false;  // Reset when exiting portal B
                }
            }
        }
    }
}
