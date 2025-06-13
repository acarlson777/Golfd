using UnityEngine;
using System.Collections;

public class DeleteJSONDataDirectory : MonoBehaviour
{
    public void DeleteJSONData()
    {
        JsonSerializer.Instance.DeleteJSONData();
        PlayerPrefs.SetString("FirstLevelScreen", "true");
        PlayerPrefs.SetString("FirstTitleScreen", "true");
        PlayerPrefs.SetString("isFirstTimeInLevel", "true");
        SceneHandler.Instance.LoadScene("TitleScreen");
    }
}
