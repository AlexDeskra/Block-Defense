using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    #region StaticVariables
    private int Health;
    private int Gold;
    private int Damage;
    private float Speed;
    private Sprite EnemySprite;
    #endregion

    #region ModifiableVariables
    private int CurrentHealth;
    public float CurrentSpeed;
    #endregion

    public Enemy(int health, int gold, int damage, float speed, Sprite enemySprite)
    {
        Health = health;
        Gold = gold;
        Damage = damage;
        Speed = speed;
        EnemySprite = enemySprite;

        CurrentHealth = Health;
        CurrentSpeed = Speed;
    }

    public Enemy(Enemy other)
    {
        Width = other.Width;
        Length = other.Length;
        Name = other.Name;
        Health = other.Health;
        Gold = other.Gold;
        Damage = other.Damage;
        Speed = other.Speed;
        EnemySprite = other.EnemySprite;

        CurrentHealth = Health;
        CurrentSpeed = Speed;
    }

    public Enemy(EnemyScriptableObject ESO)
    {
        Width = ESO.Width;
        Length = ESO.Length;
        Name = ESO.Name;
        Health = ESO.MaxHealth;
        Gold = ESO.Gold;
        Damage = ESO.Damage;
        Speed = ESO.Speed;
        EnemySprite = ESO.EnemySprite;

        CurrentHealth = Health;
        CurrentSpeed = Speed;
    }

    /// <summary>
    /// Makes enemy take Damage, returns true if the enemy's CurrentHealth is 0 or less.
    /// </summary>
    /// <param name="Damage"></param>
    /// <returns></returns>
    public bool TakeDamage(int Damage)
    {
        CurrentHealth -= Damage;
        if (CurrentHealth <= 0)
            return false;
        return true;
    }
    public int getGold() { return Gold; }
    public int getDamage() { return Damage; }
}
