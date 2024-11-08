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
    private GameObject levelModel;
    
    public void Setup(int worldID, int levelID)
    {
        GolfLevel golfLevel = JsonSerializer.Instance.golfPlayerData.WORLDS[worldID].LEVELS[levelID];
        levelNameText.GetComponent<TextMeshProUGUI>().text = golfLevel.NAME;
        parText.GetComponent<TextMeshProUGUI>().text = golfLevel.PAR.ToString();
        bestScoreText.GetComponent<TextMeshProUGUI>().text = golfLevel.bestScore.ToString();
        //GameObject levelModelPrefab = (GameObject) Resources.Load(golfLevel.LEVEL_PREFAB_NAME);
        //levelModel = Instantiate(levelModelPrefab, levelModelParent.transform);
    }
}
