using UnityEngine;
using System.Collections;

public class DeleteJSONDataDirectory : MonoBehaviour
{
    public void DeleteJSONData()
    {
        JsonSerializer.Instance.DeleteJSONData();
    }
}
