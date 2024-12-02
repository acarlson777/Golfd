using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour

{
    public List<GameObject> gameObjectsList = new List<GameObject>();
    void Start()
    {
        int randomIndex = Random.Range(0, gameObjectsList.Count);
        Instantiate(gameObjectsList[randomIndex], Vector3.zero, Quaternion.identity);
        Debug.Log("List contains " + gameObjectsList.Count + " objects.");
        
    }


    






    // Update is called once per frame
    void Update()
    {
        
    }
}

