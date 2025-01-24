using UnityEngine;

public class LoadSceneWhenThisButtonIsClicked : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneHandler.Instance.LoadScene(sceneName);
    }
}
