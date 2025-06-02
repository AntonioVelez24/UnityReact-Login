using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveData
{
    public static string Load(string saveFile)
    {
        string combinedpath = Application.streamingAssetsPath + "/" + saveFile;
        string json = "";
        if (File.Exists(combinedpath))
        {
            json = File.ReadAllText(combinedpath);
            //Debug.Log("Save file loaded successfully: " + combinedpath);
        }
        else
        {
            Debug.LogWarning("Save file does not exist");
        }
        return json;
    }

    public static void Save(string saveFile, string json)
    {
        string combinedpath = Application.streamingAssetsPath + "/" + saveFile;
        StreamWriter sw = File.CreateText(combinedpath);
        sw.Close();
        File.WriteAllText(combinedpath, json);
    }
}