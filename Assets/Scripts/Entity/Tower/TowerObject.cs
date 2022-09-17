using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    public Tower TowerEntity;
    private TowerTrigger towerTrigger;
    private GameObject TowerSpriteObject;

    private bool isActive;
    private float Timer;

    private void Awake()
    {
        isActive = false;
        Timer = 0;
        TowerEntity = new Tower();
        towerTrigger = GetComponentInChildren<TowerTrigger>();
        TowerSpriteObject = transform.GetChild(1).gameObject;
    }
    private void Update()
    {
        if (isActive)
        {
            if (CheckAttackInterval())
            {
                if (!towerTrigger.CheckIfEnemiesIsEmpty())
                {
                    GameObject Enemy = towerTrigger.GetFurthestEnemyOnPath();
                    if (Enemy)
                    {
                        AttackEnemy(Enemy);
                        Timer = 0;
                    }
                }
            }
        }
    }
    public void Initialize(TowerScriptableObject TSO)
    {
        switch (TSO.GetType().Name)
        {
            case "ArrowTowerScriptableObject":
                ArrowTowerScriptableObject Obj = TSO as ArrowTowerScriptableObject;
                TowerEntity = new ArrowTower(Obj);
                break;
        }
        TowerSpriteObject.GetComponent<SpriteRenderer>().sprite = TSO.TowerSprite;
        Timer = TowerEntity.getAttackCooldownInSeconds();
    }
    public void setIsActive(bool value)
    {
        isActive = value;
    }
    private void AttackEnemy(GameObject Enemy)
    {
        GameObject Projectile = Instantiate(TowerEntity.getTowerProjectile(), transform.position, Quaternion.identity);
        Projectile.GetComponent<Projectile>().Initialize(TowerEntity, Enemy.transform.position);
        Timer = 0;
        RotateTowards(Enemy.transform.position);
    }
    private void RotateTowards(Vector3 Point)
    {
        Vector3 CurrentPosition = transform.position;
        Point.z = 0;
        CurrentPosition.z = 0;
        Point.x -= CurrentPosition.x;
        Point.y -= CurrentPosition.y;
        float angle = Mathf.Atan2(Point.y, Point.x) * Mathf.Rad2Deg;
        TowerSpriteObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private bool CheckAttackInterval()
    {
        if (Timer >= TowerEntity.getAttackCooldownInSeconds())
        {
            return true;
        }
        Timer += Time.deltaTime;
        return false;
    }

}
