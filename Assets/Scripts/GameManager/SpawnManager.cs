using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static GameObject EnemyPrefab;
    public Vector3 SpawnLocation;
    private List<Wave> WaveList;
    private List<EnemyObject> Enemies;
    private bool isRunning;
    private bool GameEnded;
    private int WaveNumber;
    private void Awake()
    {
        Enemies = new List<EnemyObject>();
        GameEnded = false;
    }
    public void Initialize(List<Wave> waveList, int CurrentRound)
    {
        WaveList = waveList;
        WaveNumber = CurrentRound - 1;
        isRunning = false;
    }
    public bool SpawnNextWave(List<Vector3> Waypoints)
    {
        if (GameEnded) return false;
        if (WaveNumber >= WaveList.Count) return false;
        StartCoroutine(SpawnEnemies(WaveList[WaveNumber], Waypoints));
        WaveNumber++;
        return true;
    }
    public bool RemoveEnemy(EnemyObject Enemy)
    {
        if (GameEnded) return false;
        Enemies.Remove(Enemy);
        if (Enemies.Count > 0) return true;
        return false;
    }
    public void Disable()
    {
        foreach (EnemyObject Enemy in Enemies)
        {
            Destroy(Enemy.gameObject);
        }
        GameEnded = true;
    }
    private IEnumerator SpawnEnemies(Wave NextWave, List<Vector3> Waypoints)
    {
        isRunning = true;
        int EnemyCount = 0;
        while (EnemyCount < NextWave.EnemyNumber)
        {
            SpawnEnemy(NextWave.Enemy, Waypoints);
            EnemyCount++;
            if (EnemyCount == NextWave.EnemyNumber)
            {
                isRunning = false;
                break;
            }
            else yield return new WaitForSeconds(NextWave.SpawnTimer);
        }
    }
    private void SpawnEnemy(EnemyScriptableObject obj, List<Vector3> Waypoints)
    {
        GameObject Enemy = Instantiate(EnemyPrefab, SpawnLocation, Quaternion.identity);
        Enemy.GetComponent<EnemyObject>().Initialize(obj);
        Enemy.GetComponent<EnemyObject>().StartMovement(Waypoints);
        Enemies.Add(Enemy.GetComponent<EnemyObject>());
    }
    public bool getIsRunning() { return isRunning; }
}
