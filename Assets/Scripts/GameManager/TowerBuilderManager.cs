using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilderManager : MonoBehaviour
{
    public static GameObject TowerSelected;
    private static TowerScriptableObject TowerSelectedScriptableObject;
    public static GameObject TowerPrefab;
    private static int TowerCost;
    private GameGrid Grid;
    private Manager ManagerClass;

    private void Awake()
    {
        Grid = GetComponent<Manager>().getGrid();
        ManagerClass = GetComponent<Manager>();
        TowerSelectedScriptableObject = null;
    }
    private void Update()
    {
        if (TowerSelected)
        {
            MoveTowerToCursor();
        }
    }
    public bool CreateTowerToBuild(TowerScriptableObject tower)
    {
        if (ManagerClass.PlayerClass.SubtractGold(tower.Cost))
        {
            TowerCost = tower.Cost;
            TowerSelected = Instantiate(TowerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            TowerSelected.GetComponent<TowerObject>().Initialize(tower);
            TowerSelectedScriptableObject = tower;
            return true;
        }
        return false;
    }
    public void CreateTowerFromSave(TowerScriptableObject tower, Vector3 Position)
    {
        GameObject Tower = Instantiate(TowerPrefab, Position, Quaternion.identity);
        Tower.GetComponent<TowerObject>().Initialize(tower);
        Tower.GetComponent<TowerObject>().setIsActive(true);
        Grid.ConvertTowerToGrid(Position);
    }
    private void MoveTowerToCursor()
    {
        Vector3 MousePositionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MousePositionInWorld.z = 0;
        TowerSelected.transform.position = Grid.SnapPositionToGrid(MousePositionInWorld);
        CheckForInputs();
        Grid.AddTemporaryPointToGrid(MousePositionInWorld);
    }

    private void CheckForInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CheckIfEnemyPathIsBlocked(TowerSelected.transform.position) && Grid.SearchGrid(TowerSelected.transform.position, true) == 1)
            {
                ConstructTower();

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    if (!CreateTowerToBuild(TowerSelectedScriptableObject))
                    {
                        TowerSelected = null;
                    }
                }
                else TowerSelected = null;
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            ManagerClass.PlayerClass.AddGold(TowerCost);
            Destroy(TowerSelected);
        }
    }
    private void ConstructTower()
    {
        TowerSelected.GetComponent<TowerObject>().setIsActive(true);
        Grid.ConvertTowerToGrid(TowerSelected.transform.position);
        ManagerClass.UpdateGold();
        Statistics.TowersConstructed++;
    }

    private bool CheckIfEnemyPathIsBlocked(Vector3 Position)
    {
        return ManagerClass.getPathExists();
    }

}



