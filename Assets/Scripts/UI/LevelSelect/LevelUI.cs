using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject levelNameText;
    [SerializeField] private GameObject parText;
    [SerializeField] private GameObject bestScoreText;
    [SerializeField] private GameObject levelModelParent;
    [SerializeField] private string levelModelPrefabName;
    private GameObject levelModel;
    
    public void Setup(int worldID, int levelID)
    {
        GolfLevel golfLevel = JsonSerializer.Instance.golfPlayerData.WORLDS[worldID].LEVELS[levelID];
        levelNameText.GetComponent<TextMeshProUGUI>().text = golfLevel.NAME;
        parText.GetComponent<TextMeshProUGUI>().text = golfLevel.PAR.ToString();
        bestScoreText.GetComponent<TextMeshProUGUI>().text = golfLevel.bestScore.ToString();
        levelModelPrefabName = golfLevel.LEVEL_PREFAB_NAME;
        //GameObject levelModelPrefab = (GameObject) Resources.Load(levelModelPrefabName);
        //levelModel = Instantiate(levelModelPrefab, levelModelParent.transform);
    }

    private bool Equals(GolfLevel golfLevel)
    {
        if (!levelNameText.GetComponent<TextMeshProUGUI>().text.Equals(golfLevel.NAME)) { return false; }
        if (!parText.GetComponent<TextMeshProUGUI>().text.Equals(golfLevel.PAR)) { return false; }
        if (!levelModelPrefabName.Equals(golfLevel.LEVEL_PREFAB_NAME)) { return false; }

        return true;
    }

    public GolfLevel UpdateEditorChanges(GolfLevel golfLevel)
    {
        if (!Equals(golfLevel))
        {
            golfLevel.NAME = levelNameText.GetComponent<TextMeshProUGUI>().text;
            golfLevel.PAR = Int32.Parse(parText.GetComponent<TextMeshProUGUI>().text);
            golfLevel.LEVEL_PREFAB_NAME = levelModelPrefabName;
        }
        return golfLevel;
    }
}
