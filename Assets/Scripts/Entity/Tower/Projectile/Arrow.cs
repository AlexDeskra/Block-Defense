using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private bool Pierce;
    public override void OnProjectileHit(GameObject Enemy)
    {
        Enemy.GetComponent<EnemyObject>().TakeDamage(Damage);
        if (!Pierce)
            Destroy(this.gameObject);
    }
    protected override void OnDestinationReached()
    {
        Destroy(this.gameObject);
    }
    protected override Vector3 GetTarget(Vector3 Target)
    {
        Vector3 temp = CalculateDirection(Target);
        return transform.position + temp.normalized * 5;
    }
    protected override bool ProjectileTravelConditions(float Timer)
    {
        if (Timer > 0.75) return false;
        return true;
    }
    public override void Initialize(Tower TowerEntity, Vector3 Target)
    {
        ArrowTower ArrowTowerEntity = TowerEntity as ArrowTower;
        base.Initialize(TowerEntity, Target);
        Pierce = ArrowTowerEntity.Pierce;
        StartCoroutine(MoveTowardsTarget());
    }

    private void Start()
    {

    }
}
