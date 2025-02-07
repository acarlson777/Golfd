using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private GameObject levelNameText;
    [SerializeField] private GameObject parText;
    [SerializeField] private GameObject bestScoreText;
    [SerializeField] private GameObject levelModelParent;
    [SerializeField] private string levelModelPrefabName;
    [SerializeField] private Button button;
    private GameObject levelModel;
    
    public void Setup(int worldID, int levelID)
    {
        GolfLevel golfLevel = JsonSerializer.Instance.golfPlayerData.WORLDS[worldID].LEVELS[levelID];
        levelNameText.GetComponent<TextMeshProUGUI>().text = golfLevel.NAME;
        parText.GetComponent<TextMeshProUGUI>().text = golfLevel.PAR.ToString();
        bestScoreText.GetComponent<TextMeshProUGUI>().text = golfLevel.bestScore.ToString();
        levelModelPrefabName = golfLevel.LEVEL_PREFAB_NAME;
        button.onClick.AddListener(() => { EnterWorldAtThisLevel(worldID, levelID); });

        //GameObject levelModelPrefab = (GameObject) Resources.Load(levelModelPrefabName);
        //levelModel = Instantiate(levelModelPrefab, levelModelParent.transform);
    }

    private void EnterWorldAtThisLevel(int worldID, int levelID)
    {
        PlayerPrefs.SetInt((worldID+1).ToString(), levelID);
        SceneHandler.Instance.LoadScene("World " + (worldID +1).ToString());
    }

    public GolfLevel UpdateEditorChanges(GolfLevel golfLevel)
    {
        golfLevel.NAME = levelNameText.GetComponent<TextMeshProUGUI>().text;
        golfLevel.PAR = Int32.Parse(parText.GetComponent<TextMeshProUGUI>().text);
        golfLevel.LEVEL_PREFAB_NAME = levelModelPrefabName;
        return golfLevel;
    }
}
