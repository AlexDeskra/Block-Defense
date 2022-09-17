using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInitializer : MonoBehaviour
{
    public GameObject TowerPrefab;
    public GameObject TowerButtonPrefab;
    public GameObject EnemyPrefab;


    private void Awake()
    {
        TowerBuilderManager.TowerPrefab = TowerPrefab;
        UIManager.TowerButtonPrefab = TowerButtonPrefab;
        SpawnManager.EnemyPrefab = EnemyPrefab;
    }
}
