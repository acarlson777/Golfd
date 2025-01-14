using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour
{
    [SerializeField] private GameObject worldNameText;
    [SerializeField] private GameObject parText;
    [SerializeField] private GameObject bestScoreText;
    [SerializeField] private List<LevelUI> LEVEL_LIST;

    public void Setup(int worldID)
    {
        GolfWorld golfWorld = JsonSerializer.Instance.golfPlayerData.WORLDS[worldID]; //Something's wrong here
        worldNameText.GetComponent<TextMeshProUGUI>().text = (golfWorld.NAME);
        Vector2 parAndBestScoreSums = SumOfParsAndBestScores(golfWorld);
        parText.GetComponent<TextMeshProUGUI>().text = parAndBestScoreSums.x.ToString();
        bestScoreText.GetComponent<TextMeshProUGUI>().text = parAndBestScoreSums.y.ToString();

        for (int levelID = 0; levelID < LEVEL_LIST.Count; levelID++)
        {
            LEVEL_LIST[levelID].Setup(worldID, levelID);
        }
    }

    private Vector2 SumOfParsAndBestScores(GolfWorld golfWorld)
    {
        int parSum = 0;
        int bestScoreSum = 0;
        foreach (GolfLevel level in golfWorld.LEVELS)
        {
            parSum += level.PAR;
            bestScoreSum += level.bestScore;
        }
        return new Vector2(parSum, bestScoreSum);
    }

    public GolfWorld UpdateEditorChanges(GolfWorld golfWorld)
    {
        golfWorld.NAME = worldNameText.GetComponent<TextMeshProUGUI>().text;
        for (int i = 0; i < LEVEL_LIST.Count; i++)
        {
            golfWorld.LEVELS[i] = LEVEL_LIST[i].UpdateEditorChanges(golfWorld.LEVELS[i]);
        }
        return golfWorld;
    }
}
