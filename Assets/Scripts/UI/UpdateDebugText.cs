using UnityEngine;
using System.Collections;
using TMPro;

public class UpdateDebugText : MonoBehaviour
{
    [SerializeField] private GameObject debugTextObject;
    private string debugText = "";
    [SerializeField] WorldHandler worldHandler;

    private void Update()
    {
        debugText = "";
        if (worldHandler.GetCurrLevelHandler() == null) return;
        debugText += "Level Height: " + worldHandler.GetCurrLevelHandler().LEVEL.transform.position.y + "\n";
        debugText += "Floor Height: " + GameObject.FindGameObjectWithTag("WorldFloor").transform.position.y;

        debugTextObject.GetComponent<TextMeshProUGUI>().text = debugText;
    }
}