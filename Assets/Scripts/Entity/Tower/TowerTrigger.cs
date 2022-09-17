using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTrigger : MonoBehaviour
{
    private List<EnemyObject> Enemies;
    private void Awake()
    {
        Enemies = new List<EnemyObject>();
    }
    public bool CheckIfEnemiesIsEmpty()
    {
        if (Enemies.Count == 0) return true;
        return false;
    }
    public GameObject GetFurthestEnemyOnPath()
    {
        EnemyObject Enemy = Enemies[0];
        for (int i = 1; i < Enemies.Count; i++)
        {
            if (Enemy.GetTimeSinceSpawn() < Enemies[i].GetTimeSinceSpawn())
            {
                Enemy = Enemies[i];
            }
        }
        if (Enemy == null) return null;
        return Enemy.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) Enemies.Add(collision.GetComponent<EnemyObject>());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) Enemies.Remove(collision.GetComponent<EnemyObject>());
    }
}
