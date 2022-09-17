using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    private float AttackCooldownInSeconds;
    private float ProjectileVelocity;
    private int Damage;
    private GameObject TowerProjectile;
    private Sprite TowerSprite;

    public Tower(TowerScriptableObject TSO)
    {
        Name = TSO.Name;
        Damage = TSO.Damage;
        TowerProjectile = TSO.TowerProjectile;
        AttackCooldownInSeconds = TSO   .AttackCooldownInSeconds;
        TowerSprite = TSO.TowerSprite;
        ProjectileVelocity = TSO.ProjectileVelocity;
    }
    public Tower(Tower other)
    {
        Name = other.Name;
        Damage = other.Damage;
        TowerProjectile = other.TowerProjectile;
        AttackCooldownInSeconds = other.AttackCooldownInSeconds;
        TowerSprite = other.TowerSprite;
        ProjectileVelocity = other.ProjectileVelocity;
    }
    public Tower()
    {
        Name = "";
        Damage = 0;
        TowerProjectile = null;
        AttackCooldownInSeconds = 0;
        TowerSprite = null;
        ProjectileVelocity = 0;
    }

    public float getAttackCooldownInSeconds() { return AttackCooldownInSeconds; }
    public float getProjectileVelocity() { return ProjectileVelocity; }
    public int getDamage() { return Damage; }
    public GameObject getTowerProjectile() { return TowerProjectile; }
    public Sprite getTowerSprite() { return TowerSprite; }
}
