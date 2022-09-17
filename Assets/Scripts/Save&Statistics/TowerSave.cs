using UnityEngine;

[System.Serializable]
public class TowerSave
{
    public string TowerClass;
    public Vector3 TowerPosition;

    public TowerSave()
    {
        TowerClass = null;
        TowerPosition = Vector3.zero;
    }
    public TowerSave(string towerClass, Vector3 towerPosition)
    {
        TowerClass = towerClass;
        TowerPosition = towerPosition;
    }
}
