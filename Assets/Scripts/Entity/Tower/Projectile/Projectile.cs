using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected int Damage;
    protected float Velocity;
    protected Vector3 Direction;
    protected Vector3 Target;

    public abstract void OnProjectileHit(GameObject Enemy);
    protected abstract void OnDestinationReached();
    protected abstract Vector3 GetTarget(Vector3 Target);
    protected abstract bool ProjectileTravelConditions(float Timer);
    public Vector3 CalculateDirection(Vector3 Target)
    {
        Vector3 Result = (Target - transform.position);
        Result.z = 0;
        return Result.normalized;
    }
    public virtual void Initialize(Tower TowerEntity, Vector3 target)
    {
        Damage = TowerEntity.getDamage();
        Velocity = TowerEntity.getProjectileVelocity();
        Target = GetTarget(target);
    }
    protected IEnumerator MoveTowardsTarget()
    {
        float Timer = 0;
        RotateTowards(Target);
        while (true)
        {
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * Velocity);
            Timer += Time.deltaTime;
            if (!ProjectileTravelConditions(Timer)) OnDestinationReached();
            yield return null;
        }
    }
    protected void RotateTowards(Vector3 Point)
    {
        Vector3 CurrentPosition = transform.position;
        Point.z = 0;
        CurrentPosition.z = 0;
        Point.x -= CurrentPosition.x;
        Point.y -= CurrentPosition.y;
        float angle = Mathf.Atan2(Point.y, Point.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    public int GetDamage() => Damage;

    private void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Enemy")) OnProjectileHit(obj.gameObject);
    }

}
