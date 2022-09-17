using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    private Canvas SceneCanvas;
    private List<Button> TowerButtons;
    private GameObject LifeCounter;
    private GameObject GoldCounter;
    private GameObject RoundCounter;
    private GameObject TowerInfoPanel;
    private Manager ManagerEntity;
    public static GameObject TowerButtonPrefab;

    private TowerBuilderManager TowerBuilderManagerEntity;

    public float TowerButtonsDistanceX;
    public float TowerButtonsDistanceY;

    private void Awake()
    {
        SceneCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (SceneCanvas == null)
            SceneCanvas = new GameObject("Canvas").AddComponent<Canvas>();
        TowerBuilderManagerEntity = GetComponent<TowerBuilderManager>();
        ManagerEntity = GetComponent<Manager>();
    }

    public void CreateButtonList(List<TowerScriptableObject> Towers)
    {
        int index = 0;
        GameObject TowerButtonParent = new GameObject("TowerButtons", typeof(RectTransform));
        TowerButtonParent.transform.SetParent(SceneCanvas.transform);
        TowerButtonParent.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        TowerButtonParent.GetComponent<RectTransform>().anchorMax = Vector2.zero;
        TowerButtonParent.GetComponent<RectTransform>().anchoredPosition = new Vector3(50, 50, 0);
        foreach (TowerScriptableObject obj in Towers)
        {
            Button button = Instantiate(TowerButtonPrefab, SceneCanvas.transform, false).GetComponent<Button>();
            button.transform.SetParent(TowerButtonParent.transform);
            button.GetComponentInChildren<Image>().sprite = obj.TowerSprite;
            button.GetComponent<RectTransform>().anchoredPosition = new Vector3(index * TowerButtonsDistanceX, index * TowerButtonsDistanceY, 0);
            AddTowerToButton(button, obj);
            index++;
        }
    }
    public void CreateGameUI()
    {
        GameObject StartButton = Instantiate(ResourceLoader.GetUIElementFromResources("StartButton"), SceneCanvas.transform);
        GameObject QuitButton = Instantiate(ResourceLoader.GetUIElementFromResources("QuitButton"), SceneCanvas.transform);
        LifeCounter = Instantiate(ResourceLoader.GetUIElementFromResources("Life Counter"), SceneCanvas.transform);
        GoldCounter = Instantiate(ResourceLoader.GetUIElementFromResources("Gold Counter"), SceneCanvas.transform);
        RoundCounter = Instantiate(ResourceLoader.GetUIElementFromResources("RoundCounter"), SceneCanvas.transform);
        TowerInfoPanel = Instantiate(ResourceLoader.GetUIElementFromResources("TowerInfoPanel"), SceneCanvas.transform);
        StartButton.GetComponent<Button>().onClick.AddListener(ManagerEntity.StartGame);
        StartButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(-64, -16, 0);
        QuitButton.GetComponent<Button>().onClick.AddListener(ManagerEntity.BackToMenu);
        TowerInfoPanel.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        TowerInfoPanel.GetComponent<RectTransform>().anchorMax = Vector2.zero;
        TowerInfoPanel.SetActive(false);
    }
    public void UpdateHealth(int Health)
    {
        LifeCounter.GetComponentInChildren<Text>().text = Health.ToString();
    }
    public void UpdateGold(int Gold)
    {
        GoldCounter.GetComponentInChildren<Text>().text = Gold.ToString();
    }
    public void UpdateTowerInfoPanel(string Text, Vector3 Position)
    {
        TowerInfoPanel.GetComponentInChildren<Text>().text = Text;
        TowerInfoPanel.GetComponent<RectTransform>().anchoredPosition = new Vector3(100, TowerInfoPanel.GetComponentInChildren<Text>().rectTransform.rect.height / 2 + 100, 0);
        TowerInfoPanel.SetActive(true);
    }
    public void UpdateRoundCounter(int n1, int n2)
    {
        RoundCounter.GetComponent<Text>().text = "Round: " + n1.ToString() + "/" + n2.ToString();
    }
    public void DisableTowerInfoPanel()
    {
        TowerInfoPanel.SetActive(false);
    }
    public void ShowDefeatScreen()
    {
        GameObject DefeatScreen = Instantiate(ResourceLoader.GetUIElementFromResources("DefeatScreen"), SceneCanvas.transform);
        DefeatScreen.transform.Find("BackToMenuButton").GetComponent<Button>().onClick.AddListener(ManagerEntity.BackToMenu);
        DefeatScreen.transform.Find("RestartButton").GetComponent<Button>().onClick.AddListener(ManagerEntity.Restart);
        DefeatScreen.SetActive(true);
    }
    public void ShowVictoryScreen()
    {
        GameObject VictoryScreen = Instantiate(ResourceLoader.GetUIElementFromResources("VictoryScreen"), SceneCanvas.transform);
        VictoryScreen.transform.Find("BackToMenuButton").GetComponent<Button>().onClick.AddListener(ManagerEntity.BackToMenu);
        VictoryScreen.transform.Find("NextLevelButton").GetComponent<Button>().onClick.AddListener(ManagerEntity.NextLevel);
        VictoryScreen.transform.Find("RestartButton").GetComponent<Button>().onClick.AddListener(ManagerEntity.Restart);
        VictoryScreen.SetActive(true);
    }
    private void AddTowerToButton(Button button, TowerScriptableObject tower)
    {

        button.onClick.AddListener(delegate
        {
            if (!ManagerEntity.getGameStarted())
                TowerBuilderManagerEntity.CreateTowerToBuild(tower);
        });
        button.gameObject.AddComponent<TowerPanel>().Initialize(this, tower.getText());
    }

}
