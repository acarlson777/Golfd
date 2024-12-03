using UnityEngine;
using System.Collections;
using TMPro;

public class UpdateVersionNumberText : MonoBehaviour
{
    [SerializeField] private GameObject versionNumberText;

    private void Start()
    {
        versionNumberText.GetComponent<TextMeshProUGUI>().text = "v" + Application.version.ToString();
    }
}
