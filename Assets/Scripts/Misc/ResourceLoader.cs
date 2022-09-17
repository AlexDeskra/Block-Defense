using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoader
{
    public static List<TowerScriptableObject> GetTowersFromResources(string LevelName)
    {
        object[] ScriptableObjects;
        List<TowerScriptableObject> TowerList = new List<TowerScriptableObject>();
        ScriptableObjects = Resources.LoadAll("ScriptableObjects/Towers/" + LevelName, typeof(TowerScriptableObject));
        foreach (ScriptableObject obj in ScriptableObjects)
        {
            TowerList.Add(Utility.GetTowerScriptableObjectType(obj));
        }
        return TowerList;
    }
    public static List<Wave> GetWavesFromResources(string LevelName)
    {
        object[] ScriptableObjects;
        List<Wave> WaveList = new List<Wave>();
        ScriptableObjects = Resources.LoadAll("ScriptableObjects/Waves/" + LevelName, typeof(Wave));
        foreach (ScriptableObject obj in ScriptableObjects)
        {
            WaveList.Add(obj as Wave);
        }
        return WaveList;
    }
    public static GameObject GetUIElementFromResources(string Name)
    {
        object Object = Resources.Load("UI/" + Name, typeof(GameObject));
        return Object as GameObject;
    }
    public static List<GameObject> GetRocksFromResources()
    {
        object[] Objects = Resources.LoadAll("Prefabs/Rocks/", typeof(GameObject));
        List<GameObject> RockList = new List<GameObject>();
        foreach (GameObject rock in Objects)
        {
            RockList.Add(rock);
        }
        return RockList;
    }

}
