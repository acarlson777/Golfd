using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubPositionMimic : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToMimic;
    [SerializeField] private GameObject clubHeadModel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = gameObjectToMimic.transform.position;
        this.transform.rotation = gameObjectToMimic.transform.rotation;
        this.clubHeadModel.SetActive(gameObjectToMimic.activeInHierarchy);
    }
}
