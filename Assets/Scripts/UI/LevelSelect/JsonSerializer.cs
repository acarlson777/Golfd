using UnityEngine;
using System.Collections;
using System.IO;
using Unity.VisualScripting.FullSerializer;

public class JsonSerializer : MonoBehaviour
{
    public static JsonSerializer Instance { get; private set; }

    public GolfPlayerData golfPlayerData;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void SaveByJSON()
    {
        string JsonString = JsonUtility.ToJson(golfPlayerData, true);

        if (!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/")))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/"));
        }
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Saves/JSONData.text");
        sw.Write(JsonString);
        sw.Close();
        Debug.Log("==============SAVED================");
    }

    public void LoadByJSON()
    {
        if (!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/")))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/"));
        }
        if (File.Exists(Application.persistentDataPath + "/Saves/JSONData.text"))
        {
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/Saves/JSONData.text");

            string JsonString = sr.ReadToEnd();
            Debug.Log(JsonString);
            golfPlayerData = JsonUtility.FromJson<GolfPlayerData>(JsonString); //Convert JSON to the Object(GolfPlayerData)
            sr.Close();
            Debug.Log("==============LOADED================");
        }
        else
        {
            Debug.Log("FILE NOT FOUND");
        }
    }
}
