using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject GolfBallGen1;
    public GameObject GolfBall;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            // teleports the Golf ball to the Golf Ball generator
            GolfBall.transform.position = GolfBallGen1.transform.position;

        }
    }
}
