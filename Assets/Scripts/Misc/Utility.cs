using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility
{
    public static Tower GetTowerType(ScriptableObject TSO)
    {
        switch (TSO.GetType().ToString())
        {
            case "ArrowTowerScriptableObject":
                return new ArrowTower(TSO as ArrowTowerScriptableObject);
        }
        return null;
    }

    public static TowerScriptableObject GetTowerScriptableObjectType(ScriptableObject TSO)
    {
        switch (TSO.GetType().ToString())
        {
            case "ArrowTowerScriptableObject":
                return TSO as ArrowTowerScriptableObject;
        }
        return null;
    }
    public static float GetFloatNumber(float Number)
    {
        if (Number >= 0) return Number - Mathf.Floor(Number);
        else return Number - Mathf.Ceil(Number);
    }

}
