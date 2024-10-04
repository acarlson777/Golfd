using UnityEngine;
using System.Collections;

public class HoleHandler : MonoBehaviour
{
    [SerializeField] private LevelHandler _levelHandler;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GolfBall"))
        {
            StartCoroutine(_levelHandler.OnLevelCompleted());
        }
    }
}
