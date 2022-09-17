using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveFile
{
    public string Name;
    public List<TowerSave> Towers;
    public int Gold;
    public int Lives;
    public int Round;
    public string LevelName;
    public SaveFile()
    {
        Name = "";
        Towers = new List<TowerSave>();
        Gold = 0;
        Lives = 0;
        Round = 0;
        LevelName = "";
    }
    public SaveFile(string name, List<TowerSave> towers, int gold, int lives, int round, string levelName)
    {
        Name = name;
        Towers = towers;
        Gold = gold;
        Lives = lives;
        Round = round;
        LevelName = levelName;
    }
}

