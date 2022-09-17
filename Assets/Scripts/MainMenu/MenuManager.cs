using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Button ContinueButton;
    public Button NewGameButton;
    public Button SelectLevelButton;
    public Button OptionsButton;
    public Button StatisticsButton;
    public Button QuitGameButton;
    public GameObject SelectLevelScreen;
    public GameObject StatisticsScreen;
    private void Start()
    {
        ContinueButton = GameObject.Find("Continue").GetComponent<Button>();
        NewGameButton = GameObject.Find("New Game").GetComponent<Button>();
        SelectLevelButton = GameObject.Find("Select Level").GetComponent<Button>();
        StatisticsButton = GameObject.Find("Statistics").GetComponent<Button>();
        QuitGameButton = GameObject.Find("Quit Game").GetComponent<Button>();
        SelectLevelScreen = GameObject.Find("SelectLevelScreen");
        StatisticsScreen = GameObject.Find("StatisticsScreen");
        ContinueButton.onClick.AddListener(ContinueGame);
        NewGameButton.onClick.AddListener(NewGame);
        SelectLevelButton.onClick.AddListener(SelectLevel);
        StatisticsButton.onClick.AddListener(StatisticsMenu);
        QuitGameButton.onClick.AddListener(QuitGame);
        SelectLevelScreen.transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(delegate { CloseWindow(SelectLevelScreen); });
        StatisticsScreen.transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(delegate { CloseWindow(StatisticsScreen); });
        StatisticsScreen.transform.Find("ResetStats").GetComponent<Button>().onClick.AddListener(delegate { ResetStatistics(); });
        foreach (Transform obj in SelectLevelScreen.transform.Find("List").transform)
        {
            obj.GetComponent<Button>().onClick.AddListener(delegate { StartGame(obj.transform.Find("Text").GetComponent<Text>().text); });
        }
        SelectLevelScreen.SetActive(false);
        StatisticsScreen.SetActive(false);

        Statistics.LoadStatisticsSave();
    }
    private void StartGame(string LevelName)
    {
        SceneManager.LoadScene(LevelName);
    }
    private void ContinueGame()
    {
        SaveFile file = FileManager.ReadSave("AutoSave");
        if (file == null) NewGame();
        else
        {
            Manager.isLoad = true;
            StartGame(file.LevelName);
        }
    }
    private void NewGame()
    {
        Manager.isLoad = false;
        StartGame("Level 1");
    }
    private void SelectLevel()
    {
        SelectLevelScreen.SetActive(true);
    }
    private void CloseWindow(GameObject Window)
    {
        Window.SetActive(false);
    }
    private void StatisticsMenu()
    {
        StatisticsScreen.transform.Find("List").Find("EnemiesKilled").GetComponent<Text>().text = "EnemiesKilled: " + Statistics.EnemiesKilled.ToString();
        StatisticsScreen.transform.Find("List").Find("TowersConstructed").GetComponent<Text>().text = "TowersConstructed: " + Statistics.TowersConstructed.ToString();
        StatisticsScreen.transform.Find("List").Find("EnemiesLeaked").GetComponent<Text>().text = "EnemiesLeaked: " + Statistics.EnemiesLeaked.ToString();
        StatisticsScreen.transform.Find("List").Find("RoundsPlayed").GetComponent<Text>().text = "RoundsPlayed: " + Statistics.RoundsPlayed.ToString();
        StatisticsScreen.transform.Find("List").Find("GoldEarned").GetComponent<Text>().text = "GoldEarned: " + Statistics.GoldEarned.ToString();
        StatisticsScreen.transform.Find("List").Find("GamesCompleted").GetComponent<Text>().text = "GamesCompleted: " + Statistics.GamesCompleted.ToString();
        StatisticsScreen.transform.Find("List").Find("AverageLeaksPerRound").GetComponent<Text>().text = "AverageLeaksPerRound: " + Statistics.AverageLeaksPerRound.ToString("n2");
        StatisticsScreen.transform.Find("List").Find("AverageTowersPerGame").GetComponent<Text>().text = "AverageTowersPerGame: " + Statistics.AverageTowersPerGame.ToString("n2");
        StatisticsScreen.SetActive(true);
    }
    private void ResetStatistics()
    {
        Statistics.ResetStatistics();
        StatisticsScreen.transform.Find("List").Find("EnemiesKilled").GetComponent<Text>().text = "EnemiesKilled: " + Statistics.EnemiesKilled.ToString();
        StatisticsScreen.transform.Find("List").Find("TowersConstructed").GetComponent<Text>().text = "TowersConstructed: " + Statistics.TowersConstructed.ToString();
        StatisticsScreen.transform.Find("List").Find("EnemiesLeaked").GetComponent<Text>().text = "EnemiesLeaked: " + Statistics.EnemiesLeaked.ToString();
        StatisticsScreen.transform.Find("List").Find("RoundsPlayed").GetComponent<Text>().text = "RoundsPlayed: " + Statistics.RoundsPlayed.ToString();
        StatisticsScreen.transform.Find("List").Find("GoldEarned").GetComponent<Text>().text = "GoldEarned: " + Statistics.GoldEarned.ToString();
        StatisticsScreen.transform.Find("List").Find("GamesCompleted").GetComponent<Text>().text = "GamesCompleted: " + Statistics.GamesCompleted.ToString();
        StatisticsScreen.transform.Find("List").Find("AverageLeaksPerRound").GetComponent<Text>().text = "AverageLeaksPerRound: " + Statistics.AverageLeaksPerRound.ToString("n2");
        StatisticsScreen.transform.Find("List").Find("AverageTowersPerGame").GetComponent<Text>().text = "AverageTowersPerGame: " + Statistics.AverageTowersPerGame.ToString("n2");

    }
    private void QuitGame()
    {
        Application.Quit();
    }
}
