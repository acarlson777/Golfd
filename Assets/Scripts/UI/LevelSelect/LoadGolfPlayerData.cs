using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGolfPlayerData : MonoBehaviour
{
    [SerializeField] private List<WorldUI> WORLD_LIST;
    [SerializeField] private bool shouldUpdateEditorChanges;

    private void Start()
    {
        if (!JsonSerializer.Instance.DoesJSONDirectoryExist() || shouldUpdateEditorChanges)
        {
            print("Loading Editor Changes");
            ResetJSONData();
            UpdateEditorChanges();
            JsonSerializer.Instance.SaveByJSON();
        }
        
        JsonSerializer.Instance.LoadByJSON();

        for (int worldID = 0; worldID < WORLD_LIST.Count; worldID++)
        {
            print("Setting up World " + worldID);
            WORLD_LIST[worldID].Setup(worldID);
        }
    }

    private void ResetJSONData() //All worlds and levels were the same instance leading to them all being changed at the same time it's fixed now tho - C
    {
        JsonSerializer.Instance.golfPlayerData = new GolfPlayerData();
        JsonSerializer.Instance.golfPlayerData.WORLDS = new List<GolfWorld>();
        for (int i = 0; i < 3; i++)
        {
            GolfWorld golfWorld = new GolfWorld();
            golfWorld.NAME = "TEMPLATE WORLD";
            golfWorld.LEVELS = new List<GolfLevel>();
            for (int j = 0; j < 6; j++)
            {
                GolfLevel golfLevel = new GolfLevel();
                golfLevel.NAME = "TEMPLATE LEVEL";
                golfLevel.LEVEL_PREFAB_NAME = "DEFAULT_LEVEL";
                golfLevel.PAR = 1;
                golfLevel.bestScore = 1;
                golfWorld.LEVELS.Add(golfLevel);
            }
            JsonSerializer.Instance.golfPlayerData.WORLDS.Add(golfWorld);
        }
    }

    private void UpdateEditorChanges()
    {
        for (int i = 0; i < WORLD_LIST.Count; i++)
        {
            JsonSerializer.Instance.golfPlayerData.WORLDS[i] = WORLD_LIST[i].UpdateEditorChanges(JsonSerializer.Instance.golfPlayerData.WORLDS[i]);
        } 
    }
}
