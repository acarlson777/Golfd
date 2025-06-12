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

        if (golfLevel.bestScore >= 100000000)
        {
            bestScoreText.GetComponent<TextMeshProUGUI>().text = "N/A";
        }
        else if (golfLevel.bestScore > 0)
        {
            bestScoreText.GetComponent<TextMeshProUGUI>().text = "+" + golfLevel.bestScore.ToString();
        } else
        {
            bestScoreText.GetComponent<TextMeshProUGUI>().text = golfLevel.bestScore.ToString();
        }
        
        levelModelPrefabName = golfLevel.LEVEL_PREFAB_NAME;

        if (levelID == 0 && worldID == 0)
        {
            button.interactable = true;
        } else if (levelID == 0)
        {
            button.interactable = JsonSerializer.Instance.golfPlayerData.WORLDS[worldID-1].LEVELS[5].isComplete;
        } else 
        {
            button.interactable = JsonSerializer.Instance.golfPlayerData.WORLDS[worldID].LEVELS[levelID - 1].isComplete;
        }
        
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
        golfLevel.bestScore = Int32.Parse(bestScoreText.GetComponent<TextMeshProUGUI>().text);
        golfLevel.LEVEL_PREFAB_NAME = levelModelPrefabName;
        return golfLevel;
    }
}
