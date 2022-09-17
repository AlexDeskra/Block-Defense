using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/TowerScriptabeObject", order = 2)]
public class TowerScriptableObject : ScriptableObject
{
    public int ID;
    public GameObject TowerProjectile;
    public Sprite TowerSprite;
    public string Name;
    public float AttackCooldownInSeconds;
    public float ProjectileVelocity;
    public int Damage;
    public int Cost;

    public TowerScriptableObject(string name, GameObject towerProjectile, Sprite towerSprite, float attackCooldownInSeconds, int damage, float projectileVelocity, int cost, int iD)
    {
        Name = name;
        TowerProjectile = towerProjectile;
        AttackCooldownInSeconds = attackCooldownInSeconds;
        Damage = damage;
        ProjectileVelocity = projectileVelocity;
        TowerSprite = towerSprite;
        Cost = cost;
        ID = iD;
    }
    public virtual string getText()
    {
        return Name + "\nCost: " + Cost.ToString() + "\nDamage: " + Damage.ToString() + "\nAttackSpeed: " + AttackCooldownInSeconds.ToString();
    }
}
