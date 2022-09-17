using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static bool isLoad;
    public static Manager ManagerClass;
    private static string LevelName;
    private Vector3 StartLocation;
    private Vector3 Destination;
    private bool PathExists;
    private float Timer;
    private bool GameStarted;
    private bool StopGame;
    private int RoundNumber;
    public int CurrentRound;

    private AStarPathfinding Pathfinder;
    private GameGrid Grid;
    private TowerBuilderManager TowerBuilderManagerClass;
    private UIManager UIManagerClass;
    private SpawnManager SpawnManagerClass;
    private LineRenderer LineRendererClass;
    public Player PlayerClass;

    private void Awake()
    {
        InitializeClassInstances();
        Timer = 0;
        GameStarted = false;
        PathExists = false;
        StopGame = false;
        PlayerClass = new Player();
        StartLocation = GameObject.Find("StartLocation").transform.position;
        Destination = GameObject.Find("Destination").transform.position;
        SpawnManagerClass.SpawnLocation = StartLocation;
        LevelName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        AddGrassTileToGrid();
    }
    private void Start()
    {
        if (isLoad) LoadGame("AutoSave");
        else NewGame();
        Initialize();
    }
    private void InitializeClassInstances()
    {
        ManagerClass = this;
        Pathfinder = GetComponent<AStarPathfinding>();
        UIManagerClass = GetComponent<UIManager>();
        TowerBuilderManagerClass = GetComponent<TowerBuilderManager>();
        SpawnManagerClass = GetComponent<SpawnManager>();
        LineRendererClass = GetComponent<LineRenderer>();
    }
    private void Initialize()
    {
        UIManagerClass.CreateButtonList(ResourceLoader.GetTowersFromResources(LevelName));
        UIManagerClass.CreateGameUI();
        UIManagerClass.UpdateHealth(PlayerClass.getLives());
        UpdateGold();
        AddRocksToGrid();
        Grid.AddStartingLocationToGrid(StartLocation);
        List<Wave> temp = ResourceLoader.GetWavesFromResources(LevelName);
        RoundNumber = temp.Count;
        SpawnManagerClass.Initialize(temp, CurrentRound);
        Pathfinder.Init(Grid);
        UIManagerClass.UpdateRoundCounter(CurrentRound, RoundNumber);
    }
    private void Update()
    {
        List<Vector3> Path = GeneratePath();
        Timer += Time.deltaTime;
        if (Path.Count == 0) PathExists = false;
        else PathExists = true;
        if (Timer >= 0.5) DrawPath(Path);
    }
    private void AddRocksToGrid()
    {
        GameObject[] rocks = GameObject.FindGameObjectsWithTag("Terrain");
        foreach (GameObject rock in rocks)
        {
            Grid.ConvertRockToGrid(rock);
        }
    }
    private void AddGrassTileToGrid()
    {
        Tilemap tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile(pos))
            {
                if (tilemap.GetTile(pos).name == "Grass") Grid.AddGrassTileToGrid(tilemap.GetCellCenterWorld(pos));
            }
        }

    }
    private List<Vector3> GeneratePath()
    {
        return Pathfinder.GeneratePath(StartLocation, Destination, false, Vector2.zero);
    }
    private void DrawPath(List<Vector3> Waypoints)
    {
        LineRendererClass.positionCount = Waypoints.Count;
        LineRendererClass.SetPositions(Waypoints.ToArray());
    }
    private void SaveGame(string Name)
    {
        List<TowerSave> Towers = new List<TowerSave>();
        foreach (GameObject tower in GameObject.FindGameObjectsWithTag("Tower"))
        {
            TowerSave save = new TowerSave(tower.GetComponent<TowerObject>().TowerEntity.getName(), tower.transform.position);
            Towers.Add(save);
        }
        SaveFile File = new SaveFile(Name, Towers, PlayerClass.getGold(), PlayerClass.getLives(), CurrentRound, LevelName);
        FileManager.WriteSave(File);
    }
    private void LoadGame(string Name)
    {
        SaveFile File = FileManager.ReadSave(Name);
        if (File.LevelName != LevelName)
        {
            NewGame();
            return;
        }
        List<TowerScriptableObject> TowerList = ResourceLoader.GetTowersFromResources(LevelName);
        foreach (TowerSave tower in File.Towers)
        {
            TowerScriptableObject obj = TowerList.Find(x => x.Name == tower.TowerClass);
            TowerBuilderManagerClass.CreateTowerFromSave(obj, tower.TowerPosition);
        }
        PlayerClass = new Player(File.Gold, File.Lives);
        CurrentRound = File.Round;
    }
    private void NewGame()
    {
        CurrentRound = 1;
        PlayerClass = new Player();
    }
    public void StartGame()
    {
        if (!GameStarted && TowerBuilderManager.TowerSelected == null)
        {
            if (SpawnManagerClass.SpawnNextWave(GeneratePath()))
                GameStarted = true;

        }
        else GameStarted = false;
    }
    public void RoundEnd()
    {
        Statistics.RoundsPlayed++;
        Statistics.SaveStatistics();
        GameStarted = false;
        CurrentRound++;
        if (CurrentRound > RoundNumber) GameOver(true);
        else
        {
            SaveGame("AutoSave");
            UIManagerClass.UpdateRoundCounter(CurrentRound, RoundNumber);
        }
    }
    public void GameOver(bool PlayerWon)
    {
        if (StopGame) return;
        Statistics.GamesCompleted++;
        Statistics.SaveStatistics();
        GetComponent<LineRenderer>().enabled = false;
        SpawnManagerClass.Disable();
        if (!PlayerWon)
        {
            UIManagerClass.ShowDefeatScreen();
        }
        else UIManagerClass.ShowVictoryScreen();
        StopGame = true;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Restart()
    {
        isLoad = false;
        SceneManager.LoadScene(LevelName);
    }
    public void NextLevel()
    {
        isLoad = false;
        string NextLevelName = LevelName.Substring(0, LevelName.Length - 1) + (int.Parse(LevelName.Substring(LevelName.Length - 1, 1)) + 1).ToString();
        if ((int.Parse(LevelName.Substring(LevelName.Length - 1, 1)) + 1) > 4) BackToMenu();
        else
            SceneManager.LoadScene(NextLevelName);
    }
    public void UpdateGold()
    {
        UIManagerClass.UpdateGold(PlayerClass.getGold());
    }
    public void onEnemyDeath(EnemyObject Enemy)
    {
        Statistics.EnemiesKilled++;
        Statistics.GoldEarned += Enemy.getEnemyClass().getGold();
        PlayerClass.AddGold(Enemy.getEnemyClass().getGold());
        if (!SpawnManagerClass.RemoveEnemy(Enemy) && !SpawnManagerClass.getIsRunning()) RoundEnd();
        UIManagerClass.UpdateGold(PlayerClass.getGold());
    }
    public void onEnemyDestinationReach(EnemyObject Enemy)
    {
        Statistics.EnemiesLeaked++;
        if (!PlayerClass.DecreaseLives(Enemy.getEnemyClass().getDamage())) GameOver(false);
        UIManagerClass.UpdateHealth(PlayerClass.getLives());
        if (!SpawnManagerClass.RemoveEnemy(Enemy)) RoundEnd();
    }
    public bool getGameStarted() { return GameStarted; }
    public GameGrid getGrid()
    {
        if (Grid == null)
            Grid = new GameGrid(100, Vector2Int.zero, new Vector2(.64f, .64f));
        return Grid;
    }
    public bool getPathExists() { return PathExists; }
}
