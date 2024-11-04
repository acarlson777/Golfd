using UnityEngine;
using System.Collections;
using EditorAttributes;

public class WorldAndLevelSelectScreenGenerator : MonoBehaviour
{
    [SerializeField] private GolfWorld[] WORLD_LIST;
    [SerializeField] private GameObject _worldAndLevelSelectScreenPrefab;
    private GameObject worldAndLevelSelectScreen;

    [Button("Generate World And Level Select Screen")]
    private void GenerateWorldAndLevelSelectScreen()
    {
        if (worldAndLevelSelectScreen != null)
        {
            Destroy(worldAndLevelSelectScreen);
        }
        worldAndLevelSelectScreen = Instantiate(_worldAndLevelSelectScreenPrefab);
    }
}
