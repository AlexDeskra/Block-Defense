using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyScriptabeObject", order = 1)]
public class EnemyScriptableObject : ScriptableObject
{
    public int Length; // Spaces on grid
    public int Width; // Spaces on grid
    public string Name;
    public int MaxHealth;
    public int Gold;
    public int Damage;
    public float Speed;
    public Sprite EnemySprite;

    public EnemyScriptableObject()
    {
        Length = 0;
        Width = 0;
        Name = "";
        MaxHealth = 0;
        Gold = 0;
        Damage = 0;
        Speed = 0;
        EnemySprite = null;
    }

    public EnemyScriptableObject(int length, int width, string name, int maxHealth, int gold, int damage, float speed, Sprite enemySprite)
    {
        Length = length;
        Width = width;
        Name = name;
        MaxHealth = maxHealth;
        Gold = gold;
        Damage = damage;
        Speed = speed;
        EnemySprite = enemySprite;
    }
}
