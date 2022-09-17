using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Towers/ArrowTower", order = 1)]
public class ArrowTowerScriptableObject : TowerScriptableObject
{
    public bool Pierce;
    public ArrowTowerScriptableObject(int id, string name, GameObject towerProjectile, Sprite towerSprite, float attackCooldownInSeconds, float projectileVelocity, int damage, int gold, bool pierce) : base(name, towerProjectile, towerSprite, attackCooldownInSeconds, damage, projectileVelocity, gold, id)
    {
        Pierce = pierce;
    }
    public override string getText()
    {
        return base.getText() + "\nPierce: " + Pierce.ToString();
    }
}
