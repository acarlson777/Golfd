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

    public bool DoesJSONDataExist()
    {
        return File.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/GameData.json"));
    }

    public void DeleteJSONData()
    {
        if (DoesJSONDataExist())
        {
            File.Delete(Application.persistentDataPath + "/Saves/GameData.json");
        }
    }

    public void SaveByJSON()
    {
        string JsonString = JsonUtility.ToJson(golfPlayerData, true);

        if (!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/")))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/"));
        }
        StreamWriter sw = new StreamWriter(Application.persistentDataPath + "/Saves/GameData.json");
        sw.Write(JsonString);
        sw.Close();
        print(Application.persistentDataPath + "/Saves/GameData.json");
        Debug.Log("==============SAVED================");
    }

    public void LoadByJSON()
    {
        print(Application.persistentDataPath);
        if (!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/")))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/"));
        }
        if (File.Exists(Application.persistentDataPath + "/Saves/GameData.json"))
        {
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/Saves/GameData.json");

            string JsonString = sr.ReadToEnd();
            Debug.Log(JsonString);
            golfPlayerData = JsonUtility.FromJson<GolfPlayerData>(JsonString); //Convert JSON to the Object(GolfPlayerData)
            sr.Close();
            print(Application.persistentDataPath + "/Saves/GameData.json");
            Debug.Log("==============LOADED================");
        }
        else
        {
            Debug.Log("FILE NOT FOUND");
        }
    }
}
