using UnityEngine;
using System.Collections;
using System;

public class LevelPrefabCamera : MonoBehaviour
{
    private GameObject levelPrefabModelParent;
    private GameObject[] adjacentLevels = new GameObject[2];
    //Somehow have the camera constantly copy the opposite movement of whatever screen is being dragged

    public void GenerateCrossWorldLevels(Vector3 pageStep) 
    {
        //Pass in the necessary world and level indexs as a parameter
        DestroyOldAdjacentLevels();
        //if worldIndex > 0 then generate left side
            //Instantiate the prefab from the prefab name at worldUI[worldToBeAt].LEVELS[levelIndexToBeAt]
        // if worldIndex < length - 1 then generate right side
            //Instantiate the prefab from the prefab name at worldUI[worldToBeAt].LEVELS[levelIndexToBeAt]
    }

    public void GenerateSameWorldLevels(Vector3 pageStep)
    {
        //Pass in the necessary world and level indexs as a parameter
        DestroyOldAdjacentLevels();
        //if levelIndex > 0 then generate left side
            //Instantiate the prefab from the prefab name at worldUI[currWorld].LEVELS[levelIndexToBeAt]
        // if levelIndex < length - 1 then generate right side
            //Instantiate the prefab from the prefab name at worldUI[currWorld].LEVELS[levelIndexToBeAt]
    }

    private void DestroyOldAdjacentLevels()
    {
        Destroy(adjacentLevels[0]);
        Destroy(adjacentLevels[1]);
    }
}
