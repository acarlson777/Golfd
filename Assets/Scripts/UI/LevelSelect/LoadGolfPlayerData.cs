using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGolfPlayerData : MonoBehaviour
{
    [SerializeField] private List<WorldUI> WORLD_LIST;

    private void Start()
    {
        ResetJSONData();
        JsonSerializer.Instance.LoadByJSON();

        for (int worldID = 0;  worldID < WORLD_LIST.Count; worldID++)
        {
            WORLD_LIST[worldID].Setup(worldID);
        }
    }

    private void ResetJSONData()
    {
        JsonSerializer.Instance.golfPlayerData = new GolfPlayerData();
        GolfLevel golfLevel = new GolfLevel();
        golfLevel.NAME = "TEMPLATE LEVEL";
        golfLevel.LEVEL_PREFAB_NAME = "DEFAULT_LEVEL";
        golfLevel.PAR = 1;
        golfLevel.bestScore = 1;
        GolfWorld golfWorld = new GolfWorld();
        golfWorld.NAME = "TEMPLATE WORLD";
        golfWorld.LEVELS = new List<GolfLevel>() { golfLevel, golfLevel, golfLevel, golfLevel, golfLevel, golfLevel };
        JsonSerializer.Instance.golfPlayerData.WORLDS = new List<GolfWorld> { golfWorld, golfWorld, golfWorld };
        JsonSerializer.Instance.SaveByJSON();
    }
}
