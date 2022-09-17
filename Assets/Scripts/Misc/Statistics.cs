using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Statistics
{
    public static int EnemiesKilled = new int();
    public static int TowersConstructed = new int();
    public static int EnemiesLeaked = new int();
    public static int RoundsPlayed = new int();
    public static int GoldEarned = new int();
    public static int GamesCompleted = new int();
    public static float AverageLeaksPerRound = new int();
    public static float AverageTowersPerGame = new int();

    public static void UpdateAverageLeaksPerRound()
    {
        if (EnemiesLeaked == 0 || RoundsPlayed == 0) AverageLeaksPerRound = 0;
        else AverageLeaksPerRound = (float)EnemiesLeaked / (float)RoundsPlayed;
    }
    public static void UpdateAverageTowersPerGame()
    {
        if (TowersConstructed == 0 || GamesCompleted == 0) AverageTowersPerGame = 0;
        else AverageTowersPerGame = (float)TowersConstructed / (float)GamesCompleted;
    }
    public static void ResetStatistics()
    {
        EnemiesKilled = 0;
        TowersConstructed = 0;
        EnemiesLeaked = 0;
        RoundsPlayed = 0;
        GoldEarned = 0;
        GamesCompleted = 0;
        AverageLeaksPerRound = 0;
        AverageTowersPerGame = 0;
        SaveStatistics();
    }
    public static void LoadStatisticsSave()
    {
        try
        {
            StatisticsSave Save = FileManager.LoadStatisticsSave();
            EnemiesKilled = Save.EnemiesKilled;
            TowersConstructed = Save.TowersConstructed;
            EnemiesLeaked = Save.EnemiesLeaked;
            RoundsPlayed = Save.RoundsPlayed;
            GoldEarned = Save.RoundsPlayed;
            GamesCompleted = Save.GamesCompleted;
            UpdateAverageLeaksPerRound();
            UpdateAverageTowersPerGame();
        }
        catch (System.IO.FileNotFoundException e)
        {
            ResetStatistics();
        }
    }
    public static void SaveStatistics()
    {
        StatisticsSave Save = new StatisticsSave();
        Save.EnemiesKilled = EnemiesKilled;
        Save.TowersConstructed = TowersConstructed;
        Save.EnemiesLeaked = EnemiesLeaked;
        Save.RoundsPlayed = RoundsPlayed;
        Save.GoldEarned = GoldEarned;
        Save.GamesCompleted = GamesCompleted;
        FileManager.WriteStatisticsSave(Save);
    }
}
[SerializeField]
public class StatisticsSave
{
    [SerializeField]
    public int EnemiesKilled;
    [SerializeField]
    public int TowersConstructed;
    [SerializeField]
    public int EnemiesLeaked;
    [SerializeField]
    public int RoundsPlayed;
    [SerializeField]
    public int GoldEarned;
    [SerializeField]
    public int GamesCompleted;
    [SerializeField]
    public float AverageLeaksPerRound;
    [SerializeField]
    public float AverageTowersPerGame;

    public StatisticsSave()
    {
        EnemiesKilled = 0;
        TowersConstructed = 0;
        EnemiesLeaked = 0;
        RoundsPlayed = 0;
        GoldEarned = 0;
        GamesCompleted = 0;
        AverageLeaksPerRound = 0;
        AverageTowersPerGame = 0;
    }
}
