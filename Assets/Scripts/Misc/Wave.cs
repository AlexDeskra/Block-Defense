using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Wave", order = 1)]
public class Wave : ScriptableObject
{
    public EnemyScriptableObject Enemy;
    public int EnemyNumber;
    public float SpawnTimer;

    Wave(float spawnTimer)
    {
        Enemy = null;
        EnemyNumber = 0;
        SpawnTimer = 0;
    }
    public Wave(EnemyScriptableObject enemy, int enemyNumber, float spawnTimer)
    {
        EnemyNumber = enemyNumber;
        Enemy = enemy;
        SpawnTimer = spawnTimer;
    }
}
