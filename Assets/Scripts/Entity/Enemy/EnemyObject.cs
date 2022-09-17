using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObject : MonoBehaviour
{
    private Enemy EnemyClass;
    private bool isDead;

    private EnemyMovement EM;
    private float TimeSinceSpawn;

    private void Awake()
    {
        EM = GetComponent<EnemyMovement>();
        TimeSinceSpawn = 0;
        isDead = false;
    }

    private void Update()
    {
        TimeSinceSpawn += Time.deltaTime;
    }

    public void Initialize(EnemyScriptableObject ESO)
    {
        EnemyClass = new Enemy(ESO);
        GetComponent<SpriteRenderer>().sprite = ESO.EnemySprite;
    }

    public void TakeDamage(int Damage)
    {
        if (!EnemyClass.TakeDamage(Damage) && isDead == false)
        {
            isDead = true;
            onDeath();
        }
    }

    public void StartMovement(List<Vector3> Waypoints)
    {
        EM.StartMovement(Waypoints, EnemyClass.CurrentSpeed);
    }
    public void onEnemyDestinationReach()
    {
        Manager.ManagerClass.onEnemyDestinationReach(this);
        Destroy(this.gameObject);
    }

    private void onDeath()
    {
        Manager.ManagerClass.onEnemyDeath(this);
        Destroy(this.gameObject);
    }
    public float GetTimeSinceSpawn()
    {
        return TimeSinceSpawn;
    }
    public Enemy getEnemyClass() { return EnemyClass; }


}
