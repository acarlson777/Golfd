using UnityEngine;
using System.Collections;

public class ClubDestructionHandler : MonoBehaviour
{
    [SerializeField] private float lifetime;

    void Start()
    {
        StartCoroutine(WaitToDestroy());
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}