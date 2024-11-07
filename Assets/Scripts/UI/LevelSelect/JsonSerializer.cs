using UnityEngine;
using System.Collections;
using System.IO;

public static class JsonSerializer
{
    public static GolfPlayerData golfPlayerData;

    public static void SaveByJSON()
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


    public static void LoadByJSON()
    {
        if (!Directory.Exists(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/")))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(Application.persistentDataPath + "/Saves/"));
        }
        if (File.Exists(Application.persistentDataPath + "/Saves/JSONData.text"))
        {
            //LOAD THE GAME
            StreamReader sr = new StreamReader(Application.persistentDataPath + "/Saves/JSONData.text");

            string JsonString = sr.ReadToEnd();

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
