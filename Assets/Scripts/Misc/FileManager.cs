using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class FileManager
{
    public static void WriteSave(SaveFile Save)
    {
        WriteFile(JsonUtility.ToJson(Save), Application.persistentDataPath + Save.Name + ".json");
    }
    public static SaveFile ReadSave(string SaveName)
    {
        string obj = ReadFile(Application.persistentDataPath + SaveName + ".json");
        SaveFile Save = JsonUtility.FromJson<SaveFile>(obj);
        return Save;
    }
    public static void WriteStatisticsSave(StatisticsSave Save)
    {
        WriteFile(JsonUtility.ToJson(Save), Application.persistentDataPath + "Statistics.json");
    }
    public static StatisticsSave LoadStatisticsSave()
    {
        string obj = ReadFile(Application.persistentDataPath + "Statistics.json");
        StatisticsSave Save = JsonUtility.FromJson<StatisticsSave>(obj);
        return Save;
    }
    private static void WriteFile(string Content, string Path)
    {
        System.IO.File.WriteAllText(Path, Content);
    }
    private static string ReadFile(string Path)
    {
        return System.IO.File.ReadAllText(Path);
    }
}
